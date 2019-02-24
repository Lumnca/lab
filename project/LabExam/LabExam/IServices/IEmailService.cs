using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabExam.IServices
{
    public interface IEmailService
    {
        void SendEmail(string receiver, string subject, string content);

        void SendJoinEmail(String receiver,String id, String name, DateTime submiTime, Boolean result, String why);

        void SendReEmail(String receiver, String id, String name, DateTime submiTime, Boolean result,
            String why = "综合你学习行为得出的结果！");
    }
}
