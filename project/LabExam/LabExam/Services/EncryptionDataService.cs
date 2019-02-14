using LabExam.IServices;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace LabExam.Services
{
    public class EncryptionDataService:IEncryptionDataService
    {
        private const String EncodeKey = "sicnu_lab_505";
        /// <summary>
        ///  使用对称加密 密钥为上面
        /// </summary>
        /// <param name="Data">待加密数据</param>
        /// <returns>已经加密数据</returns>
        public string EncodeByRsa(string Data)
        {
            CspParameters param = new CspParameters();
            param.KeyContainerName = EncodeKey;//密匙容器的名称，保持加密解密一致才能解密成功
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(param))
            {
                byte[] plaindata = Encoding.Default.GetBytes(Data);//将要加密的字符串转换为字节数组
                byte[] encryptdata = rsa.Encrypt(plaindata, false);//将加密后的字节数据转换为新的加密字节数组
                return Convert.ToBase64String(encryptdata);//将加密后的字节数组转换为字符串
            }
        }

        /// <summary>
        /// 对RSA加密后的数据进行解密
        /// </summary>
        /// <param name="EncodedData">已经加密数据</param>
        /// <returns>原来的数据</returns>
        public string DecryptByRsa(string EncodedData)
        {
            CspParameters param = new CspParameters();
            param.KeyContainerName = EncodeKey;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(param))
            {
                byte[] encryptdata = Convert.FromBase64String(EncodedData);
                byte[] decryptdata = rsa.Decrypt(encryptdata, false);
                return Encoding.Default.GetString(decryptdata);
            }
        }


        /// <summary>
        ///  <remarks> 通过MD5的方法对数据进行加密  </remarks>
        ///  <Create> 2018/9/6 19:55 </Create>
        ///  <Author> 2016110418 蒋星 </Author>
        ///  <LastAlterTimeAndAuthor>  </LastAlterTimeAndAuthor>
        /// </summary>
        public String EncodeByMd5(string Data)
        {
            if (String.IsNullOrEmpty(Data))
            {
                return "";
            }
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] palindata = Encoding.Default.GetBytes(Data);   //将要加密的字符串转换为字节数组
            byte[] encryptdata = md5.ComputeHash(palindata);      //将字符串加密后也转换为字符数组
            return Convert.ToBase64String(encryptdata);           //将加密后的字节数组转换为加密字符串
        }

        public string EncodeByMd5Times(string Data, int Time)
        {
            try
            {
                if (Time < 1)
                {
                    throw  new ArgumentException("整形参数不能小于1");
                }
                for (int i = 0; i < Time; i++)
                {
                    Data = EncodeByMd5(Data);
                }
                return Data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
