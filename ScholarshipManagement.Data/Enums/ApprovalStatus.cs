using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScholarshipManagement.Data.Enums
{
    public enum ApprovalStatus
    {
        [Description("Submitted")]
        Submitted = 1,
        [Description("Committee")]
        Committee = 2,
        [Description("NaibAmir")]
        NaibAmir = 3,
        [Description("Amir")]
        Amir = 4,
        [Description("Approved")]
        Approved = 5,
        [Description("Accounts")]
        Accounts = 6,
        [Description("Disbursed")]
        Disbursed = 7,
        [Description("Declined")]
        Declined = 8,
        [Description("Closed")]
        Closed = 9,
        [Description("In_Progress")]
        In_Progress = 10

    }
}
