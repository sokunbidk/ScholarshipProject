using System.Threading.Tasks;

namespace ScholarshipManagement.Data.Services
{
    public interface IStudentService
    {
        public Task<BaseResponse> CreateStudentAsync(CreateStudentRequestModel model,string currentUserEmail);

        public Task<BaseResponse> UpdateStudentAsync(int id, UpdateStudentRequestModel model);

        //public Task<IList<UpdateApplicationRequestModel>> GetStudentApplicationFormsAsync();

        Task<StudentsResponseModel> GetStudents(StudentsResponseModel model);
       // public Task<StudentViewModel> GetStudentAsync(StudentViewModel model);

        Task<StudentViewModel> GetStudentReturningCandidate(string email);

        Task<StudentResponseModel> GetApplicantById(int id);
        
    }
}
