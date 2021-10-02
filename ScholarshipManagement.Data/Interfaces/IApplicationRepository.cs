using ScholarshipManagement.Data.Entities;
using System.Threading.Tasks;

namespace ScholarshipManagement.Data.Interfaces
{
    public interface IApplicationRepository: IRepository<application>
    {
        Task<application> GetApplication(int id);

        Task<application> GetApplicationAsync(int applicationid);

        Task<application> GetApplicationByMemberCodeAsync(string memberCode);
        //ExistsAsync
    }
}
