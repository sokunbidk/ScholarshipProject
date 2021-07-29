using System.Threading.Tasks;

namespace ScholarshipManagement.Data.Services
{
    public interface IStudentService
    {
        public Task<BaseResponse> CreateStudentAsync(CreateStudentRequestModel model);

        public Task<BaseResponse> UpdateStudentAsync(int id, UpdateStudentRequestModel model);

        //public Task<IList<UpdateApplicationRequestModel>> GetStudentApplicationFormsAsync();

        Task<StudentsResponseModel> GetStudents(StudentsResponseModel model);

        Task<StudentResponseModel> GetStudent(int id);
    }
}
