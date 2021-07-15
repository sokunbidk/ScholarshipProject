using ScholarshipManagement.Data.DTOs;
using ScholarshipManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace ScholarshipManagement.Data.Interfaces
{
    public interface IApplicationFormRepository: IRepository<ApplicationForm>
    {
        Task<ApplicationForm> GetApplicationForm(Guid id);

        Task<ApplicationForm> GetApplicationFormAsync(Guid applicationFormNumber);

        Task<ApplicationForm> GetApplicationFormByMemberCodeAsync(string memberCode);

    }
}
