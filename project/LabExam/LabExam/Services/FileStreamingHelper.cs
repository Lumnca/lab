﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LabExam.Services
{
    public static class FileStreamingHelper
    {
        private static readonly FormOptions _defaultFormOptions = new FormOptions();

        public static async Task<FormValueProvider> StreamFiles(this HttpRequest request, string targetDirectory,string fileNewName)
        {
            if (!MultipartRequestHelper.IsMultipartContentType(request.ContentType))
            {
                throw new Exception($"Expected a multipart request, but got {request.ContentType}");
            }

            // Used to accumulate all the form url encoded key value pairs in the 
            // request.
            var formAccumulator = new KeyValueAccumulator();

            var boundary = MultipartRequestHelper.GetBoundary(
                MediaTypeHeaderValue.Parse(request.ContentType.ToLowerInvariant()),
                _defaultFormOptions.MultipartBoundaryLengthLimit);
            var reader = new MultipartReader(boundary, request.Body);

            var section = await reader.ReadNextSectionAsync();//用于读取Http请求中的第一个section数据
            while (section != null)
            {
                ContentDispositionHeaderValue contentDisposition;
                var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out contentDisposition);

                if (hasContentDispositionHeader)
                {
                    #region 注释
                    /*
                    用于处理上传文件类型的的section
                    -----------------------------99614912995
                    Content - Disposition: form - data; name = "files"; filename = "Misc 002.jpg"

                    ASAADSDSDJXCKDSDSDSHAUSAUASAASSDSDFDSFJHSIHFSDUIASUI+/==
                    -----------------------------99614912995
                    */
                    #endregion

                    if (MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
                    {
                        if (!Directory.Exists(targetDirectory))
                        {
                            Directory.CreateDirectory(targetDirectory);
                        }

                        var fileName = MultipartRequestHelper.GetFileName(contentDisposition);

                        var loadBufferBytes = 1024;//这个是每一次从Http请求的section中读出文件数据的大小，单位是Byte即字节，这里设置为1024的意思是，每次从Http请求的section数据流中读取出1024字节的数据到服务器内存中，然后写入下面targetFileStream的文件流中，可以根据服务器的内存大小调整这个值。这样就避免了一次加载所有上传文件的数据到服务器内存中，导致服务器崩溃。

                        using (var targetFileStream = System.IO.File.Create(targetDirectory + "\\" + fileNewName))
                        {
                            //section.Body是System.IO.Stream类型，表示的是Http请求中一个section的数据流，从该数据流中可以读出每一个section的全部数据，所以我们下面也可以不用section.Body.CopyToAsync方法，而是在一个循环中用section.Body.Read方法自己读出数据（如果section.Body.Read方法返回0，表示数据流已经到末尾，数据已经全部都读取完了），再将数据写入到targetFileStream
                            await section.Body.CopyToAsync(targetFileStream, loadBufferBytes);
                        }

                    }

                    #region 注释
                    /*
                    用于处理表单键值数据的section
                    -----------------------------99614912995
                    Content - Disposition: form - data; name = "SOMENAME"

                    Formulaire de Quota
                    -----------------------------99614912995
                    */
                    #endregion
                    else if (MultipartRequestHelper.HasFormDataContentDisposition(contentDisposition))
                    {
                        // Content-Disposition: form-data; name="key"
                        //
                        // value

                        // Do not limit the key name length here because the 
                        // multipart headers length limit is already in effect.
                        var key = HeaderUtilities.RemoveQuotes(contentDisposition.Name);
                        var encoding = GetEncoding(section);
                        using (var streamReader = new StreamReader(
                            section.Body,
                            encoding,
                            detectEncodingFromByteOrderMarks: true,
                            bufferSize: 1024,
                            leaveOpen: true))
                        {
                            // The value length limit is enforced by MultipartBodyLengthLimit
                            var value = await streamReader.ReadToEndAsync();
                            if (String.Equals(value, "undefined", StringComparison.OrdinalIgnoreCase))
                            {
                                value = String.Empty;
                            }
                            formAccumulator.Append(key.Value, value); // For .NET Core <2.0 remove ".Value" from key

                            if (formAccumulator.ValueCount > _defaultFormOptions.ValueCountLimit)
                            {
                                throw new InvalidDataException($"Form key count limit {_defaultFormOptions.ValueCountLimit} exceeded.");
                            }
                        }
                    }
                }

                // Drains any remaining section body that has not been consumed and
                // reads the headers for the next section.
                section = await reader.ReadNextSectionAsync();//用于读取Http请求中的下一个section数据
            }

            // Bind form data to a model
            var formValueProvider = new FormValueProvider(
                BindingSource.Form,
                new FormCollection(formAccumulator.GetResults()),
                CultureInfo.CurrentCulture);

            return formValueProvider;
        }

        private static Encoding GetEncoding(MultipartSection section)
        {
            MediaTypeHeaderValue mediaType;
            var hasMediaTypeHeader = MediaTypeHeaderValue.TryParse(section.ContentType, out mediaType);
            // UTF-7 is insecure and should not be honored. UTF-8 will succeed in 
            // most cases.
            if (!hasMediaTypeHeader || Encoding.UTF7.Equals(mediaType.Encoding))
            {
                return Encoding.UTF8;
            }
            return mediaType.Encoding;
        }
    }
}
