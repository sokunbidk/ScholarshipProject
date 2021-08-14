using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScholarshipManagement.Data.Services
{
    public interface IMailSender
    {
        public Task SendWelcomeMail(string email, string name, string userPassword);

        public Task SendForgotPasswordMail(string email, string name, string passwordResetLink);

        public Task SendChangePasswordMail(string email, string name, string userPassword);

        public Task SendResetPasswordMail(string email, string name, string userPassword);

        public Task SendVerifyMail(string email, string name);
    }
}
