using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabExam.IServices;
using System.Net.Mail;

namespace LabExam.Services
{
    public class EmailService: IEmailService
    {
        public System.Net.Mail.SmtpClient client = null;

        public void SendEmail(string receiver, string subject, string content)
        {
            if (string.IsNullOrEmpty(receiver) || string.IsNullOrEmpty(subject)
                                               || string.IsNullOrEmpty(content))
            {
                throw new ArgumentNullException("sendEmail 参数空异常！");
            }
            if (client == null)
            {
                try
                {
                    //163发送配置                    
                    client = new System.Net.Mail.SmtpClient
                    {
                        Host = "smtp.163.com",
                        Port = 25,
                        DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                        EnableSsl = true,
                        UseDefaultCredentials = true,
                        Credentials = new System.Net.NetworkCredential("kickergod@163.com", "sicnu505")
                    };
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            try
            {
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage
                {
                    SubjectEncoding = System.Text.Encoding.UTF8,
                    BodyEncoding = System.Text.Encoding.UTF8,
                    Priority = System.Net.Mail.MailPriority.High,
                    From = new System.Net.Mail.MailAddress("kickergod@163.com", "川师大实验室安全考试官方邮件")
                };

                //添加邮件接收人地址
                string[] receivers = receiver.Split(new char[] { ',' });
                Array.ForEach(receivers.ToArray(), toMail => { message.To.Add(toMail); });

                message.Subject = subject;
                message.Body = content;
                message.IsBodyHtml = true;
                client.Send(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SendJoinEmail(String receiver,String id, String name, DateTime submiTime,Boolean result,String why = "")
        {
            if (result)
            {
                var str = $"<p><small> 学号为 <strong>{id}</strong>的{name}同学于{submiTime.ToLocalTime()}时间提交的考试加入申请已经通过</small></p>" +
                          $"<div><small>你现在已经可以登录系统！请先进行密码修改.再进入系统学习和进行考试</small></div>" +
                          $"<br/><br/> <hr/> <div><small>回复时间: {DateTime.Now.ToLocalTime()}</small></div>";
                SendEmail(receiver, "[学生加入考试申请结果]", str);
            }
            else
            {
                var str = $"<p><small> 学号为 <strong>{id}</strong>的{name}同学于{submiTime.ToLocalTime()}时间提交的考试加入申请审核并未通过</small></p>" +
                          $"<div><small><b>可能原因:{why}</b></small></div>" +
                          $"<br/><br/> <hr/> <div><small>回复时间: {DateTime.Now.ToLocalTime()}</small></div>";
                SendEmail(receiver, "[学生加入考试申请结果]", str);
            }
        }

        public void SendReEmail(String receiver, String id, String name, DateTime submiTime, Boolean result, String why = "综合你学习行为得出的结果！")
        {
            if (result)
            {
                var str = $"<p><small> 学号为 <strong>{id}</strong>的{name}同学于{submiTime.ToLocalTime()}时间提交增加考试次数的重考申请已经通过</small></p>" +
                          $"<div><small>如果你尚未通过考试,请抓紧学习通过考试！</small></div>" +
                          $"<br/><br/> <hr/> <div><small>回复时间: {DateTime.Now.ToLocalTime()}</small></div>";
                SendEmail(receiver, "[重考申请结果]", str);
            }
            else
            {
                var str = $"<p><small> 学号为 <strong>{id}</strong>的{name}同学于{submiTime.ToLocalTime()}时间提交增加考试次数的重考申请未通过</small></p>" +
                          $"<div><small><b>可能原因:{why}</b></small></div>" +
                          $"<br/><br/> <hr/> <div><small>回复时间: {DateTime.Now.ToLocalTime()}</small></div>";
                SendEmail(receiver, "[重考申请结果]", str);
            }
        }

    }
}
