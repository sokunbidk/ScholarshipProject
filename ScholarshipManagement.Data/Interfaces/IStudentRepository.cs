using ScholarshipManagement.Data.DTOs;
using ScholarshipManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace ScholarshipManagement.Data.Interfaces
{
    public interface IStudentRepository: IRepository<Student>
    {
        Task<Student> GetStudentAsync(string memberCode);

        Task<Student> GetStudent(int id);

        //Task<IList<UpdateRequestModel>> GetStudentApplicationFormsAsync();

        //Task<IList<StudentDto>> GetStudentsAsync(Guid studentId);

        //Task<IList<StudentDto>> GetStudentsAsync(string memberCode);

        //Task<IList<StudentDto>> GetStudentAsync(Guid studentId);

        Task<IList<PaymentDto>> GetStudentPaymentsAsync(string memberCode);


    }
}
