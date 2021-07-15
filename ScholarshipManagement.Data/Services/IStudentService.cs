using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScholarshipManagement.Data.Services
{
    public interface IStudentService
    {
        public Task<BaseResponse> CreateStudentAsync(CreateStudentRequestModel model);
        public Task<BaseResponse> UpdateStudentAsync(Guid id, UpdateStudentRequestModel model);

        Task<StudentsResponseModel> GetStudent();
        Task<StudentResponseModel> GetStudent(Guid id);
    }
}
