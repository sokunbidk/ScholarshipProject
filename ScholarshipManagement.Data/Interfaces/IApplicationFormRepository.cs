using ScholarshipManagement.Data.Entities;
using System.Threading.Tasks;

namespace ScholarshipManagement.Data.Interfaces
{
    public interface IApplicationFormRepository: IRepository<ApplicationForm>
    {
        Task<ApplicationForm> GetApplicationForm(int id);

        Task<ApplicationForm> GetApplicationFormAsync(int applicationFormNumber);

        Task<ApplicationForm> GetApplicationFormByMemberCodeAsync(string memberCode);
    }
}
