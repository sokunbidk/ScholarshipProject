using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScholarshipManagement.Data.Enums
{
    public enum UserType
    {
        [Description("Student")]
        Student = 1,
        [Description("Circuit")]
        Circuit = 2,
        [Description("Committee")]
        Committee = 3,
        [Description("NaibAmir")]
        NaibAmir = 4,
        [Description("Amir")]
        Amir = 5,
        [Description("Accounts")]
        Accounts = 6,
        [Description("Secretariat")]
        Secretariat =7,
        [Description("Admin")]
        Admin = 8
    }
}
