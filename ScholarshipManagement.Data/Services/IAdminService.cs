﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScholarshipManagement.Data.Services
{
    public interface IAdminService
    {
        Task<StudentViewModel> GetPendingApplications(string email);
    }
}
