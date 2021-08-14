using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScholarshipManagement.Data.Models
{
    public class EmailModel
    {
        public class WelcomeEmail
        {
            public string Name { get; set; }

            public string ActivationLink { get; set; }

            public string UserPassword { get; set; }
        }

        public class ForgotPasswordEmail
        {
            public string Name { get; set; }

            public string PasswordResetLink { get; set; }
        }

        public class ResetPasswordEmail
        {
            public string Name { get; set; }

            public string UserPassword { get; set; }
        }

        public class VerifyMail
        {
            public string Name { get; set; }

            public string Email { get; set; }
        }

        public class ChangePasswordMail
        {
            public string Name { get; set; }

            public string UserPassword { get; set; }
        }
    }
}
