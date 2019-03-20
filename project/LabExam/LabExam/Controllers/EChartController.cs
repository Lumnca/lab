using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LabExam.DataSource;
using LabExam.IServices;
using LabExam.Models.Map;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DevTool;
using LabExam.Models.EntitiyViews;

namespace LabExam.Controllers
{
    [Authorize(Roles = "Principal")]
    public class EChartController : Controller
    {
        private readonly LabContext _context;
        private readonly IHttpContextAnalysisService _analysis;
        private readonly ILoggerService _logger;
        private readonly IDataBaseService _service;

        public EChartController(LabContext context, IHttpContextAnalysisService analysis, ILoggerService logger, IDataBaseService service)
        {
            _context = context;
            _analysis = analysis;
            _logger = logger;
            _service = service;
        }

        /// <summary>
        ///  这是我再听的歌: 我是真的爱你 
        /// </summary>
        /// <returns>记录了多少天的登录情况</returns>
        [HttpGet]
        public IActionResult Login()
        {
            int count = 0;
            DateTime min = _context.LogUserLogin.Min(m => m.LoginTime); //一定要登录了 有记录 不然会出错
            DateTime now = DateTime.Now;
            TimeSpan span =  now - min;
            Console.WriteLine(Math.Ceiling(span.TotalDays));
             count = (int)Math.Ceiling(span.TotalDays);
            Dictionary<String,Int32> map = _context.VUserLoginStatisticMaps.ToDictionary(vm => vm.DateLogin,vm2 => vm2.LoginCount);
            
            return Json(new
            {
                length = count,
                min = min,
                data = map
            });
        }

        [HttpGet]
        public IActionResult Distribute()
        {
            List<KeyValue> data = new List<KeyValue>();
            List<KeyValue> terminals = new List<KeyValue>();

            foreach (var val in _context.VStudentDistributionMaps.ToList())
            {
                String name = $"{val.Grade}级" + (val.StudentType == StudentType.UnderGraduate ? "本科生" : "研究生");
               
                KeyValue ky = new KeyValue(name,val.SumStudent);
                data.Add(ky);
            }

            foreach (var val in _context.VUserLoginTerminalMaps.ToList())
            {
                String key = val.Terminal == Terminal.Pc ? "电脑终端" : (val.Terminal == Terminal.Phone ? "手机终端" : "平板终端");
                KeyValue ky = new KeyValue(key , val.LoginCount);
                terminals.Add(ky);
            }

            return Json(new
            {
                data = data,
                terminals = terminals
            });
        }

        [HttpGet]
        public IActionResult Institute()
        {
            List<Int32> vals = new List<int>();
            List<String> names = new List<string>();

            foreach (var sta in _context.VInstituteStatisticMaps.ToList())
            {
                vals.Add(sta.StudentCount);
                names.Add(sta.Name);
            }
            return Json(new
            {
                vals = vals,
                names = names
            });
        }

        [HttpGet]
        public IActionResult Pass([Required] int grade = 0)
        {
            List<String> names = new List<string>();
            List<Int32> valuePass = new List<Int32>();
            List<Int32> valueNotPass = new List<Int32>();
            if (grade < 2016)
            {
                foreach (var v in _context.VExamAllStatisticMaps)
                {
                    names.Add(v.Name);
                    valuePass.Add(v.PassScount);
                    valueNotPass.Add(v.StuNotPassCount);
                }
                return Json(new
                {
                    names = names,
                    va1Pass = valuePass,
                    va1NotPass = valueNotPass
                });
            }
            else
            {

                foreach (var v in _context.VExamResultMaps.Where(v => v.Grade == grade))
                {
                    names.Add(v.Name);
                    valuePass.Add(v.PassTotal);
                    valueNotPass.Add(v.Total - v.PassTotal);
                }

                return Json(new
                {
                    names = names,
                    va1Pass = valuePass,
                    va1NotPass = valueNotPass
                });
            }
        }

        [HttpGet]
        public IActionResult Exam()
        {
            int countPass = _context.Student.Count(s => s.IsPassExam == true); //完成考试
            int countLearningFinish = _context.Learings.Count(v => v.IsFinish == true); //有多少个学习任务被完成了
            int countLearningNotFinish = _context.Learings.Count(v => v.IsFinish == false); //有多少个学习任务未完成
            int notLearning = _service.NotLearningCount(_context);
            int notJoinSystemLearning = _service.NotJoinSystemStudentCount(_context);
            List<List<float>> pair = new List<List<float>>();

            foreach (var v in _context.VExamGradeStatisticMaps)
            {
                List<float> vals = new List<float>();
                vals.Add(v.MaxExamScore);
                vals.Add(v.Count);
                pair.Add(vals);
            }

            return Json(new
            {
                pass = countPass,
                learningFinish = countLearningFinish,
                learningNotFinish = countLearningNotFinish,
                notLearning = notJoinSystemLearning - notLearning,
                notJoinSystemLearning = notJoinSystemLearning,
                pairs = pair
            });
        }

    }
}
