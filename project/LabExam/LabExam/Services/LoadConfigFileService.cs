using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LabExam.IServices;
using LabExam.Models.JsonModel;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LabExam.Services
{
    public class LoadConfigFileService: ILoadConfigFileService
    {
        private readonly IHostingEnvironment _hosting;

        public LoadConfigFileService(IHostingEnvironment hosting)
        {
            _hosting = hosting;
        }

        public SystemSetting LoadSystemSetting()
        {
            try
            {
                using (var stream = new FileStream(Path.GetFullPath($@"{_hosting.ContentRootPath}/SettingConfig.json"),
                    FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        String json = reader.ReadToEnd();
                        SystemSetting setting = JsonConvert.DeserializeObject<SystemSetting>(json);
                        return setting;
                    };
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void ReWriteSystemSetting(SystemSetting setting)
        {
            System.IO.File.WriteAllText(
                Path.GetFullPath($@"{_hosting.ContentRootPath}/SettingConfig.json"),
                JsonConvert.SerializeObject(setting, Formatting.Indented));
        }

        public Dictionary<int, ExamOpenSetting> LoadModuleExamOpenSetting()
        {
            try
            {
                using (var stream = new FileStream(Path.GetFullPath($@"{_hosting.ContentRootPath}/ModuleConfig.json"),
                    FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        String json = reader.ReadToEnd();
                        Dictionary<int, ExamOpenSetting> setting = JsonConvert.DeserializeObject<Dictionary<int, ExamOpenSetting>>(json);
                        return setting;
                    };
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void ReWriteModuleExamOpenSetting(Dictionary<int, ExamOpenSetting> setting)
        {
            System.IO.File.WriteAllText(
                Path.GetFullPath($@"{_hosting.ContentRootPath}/ModuleConfig.json"),
                JsonConvert.SerializeObject(setting, Formatting.Indented));
        }

        public PrincipalConfig LoadPrincipalConfig(String principalId)
        {
            try
            {
                if (!File.Exists(Path.GetFullPath($@"{_hosting.ContentRootPath}/JsonConfig/{principalId}.json")))
                {
                    throw new Exception($"账号为{principalId}的管理员的配置文件丢失！");
                }
                using (var stream = new FileStream(Path.GetFullPath($@"{_hosting.ContentRootPath}/JsonConfig/{principalId}.json"),
                    FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        String json = reader.ReadToEnd();
                        PrincipalConfig config = JsonConvert.DeserializeObject<PrincipalConfig>(json);
                        return config;
                    };
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }
}
