using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ScholarshipManagement.Data.ApplicationContext;
using ScholarshipManagement.Data.DTOs;
using ScholarshipManagement.Data.Entities;
using ScholarshipManagement.Data.Exceptions;
using ScholarshipManagement.Data.Interfaces;

namespace ScholarshipManagement.Data.Repositories
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        private readonly IUserRepository _userRepository;

        public StudentRepository(SchoolDbContext context,IUserRepository userRepository )
        {
            DbContext = context;
            _userRepository = userRepository;
        }

        public async Task<Student> GetStudentAsync(string memberCode)
        {
            return await Query().SingleOrDefaultAsync(u => u.User.MemberCode == memberCode);
            //return await DbContext.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
        }
        
        public async Task<Student> GetStudent(int id)
        {
            return await Query().SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Student> GetStudentByEmail(string email)
        { 
            //var user =  _userRepository.GetUserAsync(email);
            return await Query().SingleOrDefaultAsync(u => u.EmailAddress == email);
        }
        //Use latter
        public async Task<IList<UpdateApplicationRequestModel>> GetStudentApplicationFormsAsync()
        {
            return await DbContext.Applications
                
                .Include(uc => uc.Student)
                //.Where(u => u.Student.CircuitId == u.UserId
                .Select(uc => new UpdateApplicationRequestModel
                {
                    
                }).ToListAsync();
        }


       

       

        public async Task<IList<PaymentDto>> GetStudentPaymentsAsync(string memberCode)
        {

            throw new NotFoundException();
        }

        public async Task<Student> GetStudentByMemberCodeAsync(string memberCode)
        {
            return await Query().SingleOrDefaultAsync(u => u.MemberCode.Equals(memberCode));
        }

        public async Task<Student> GetStudentWithJamatByIdAsync(int id)
        {
            return await Query().Include(s => s.Jamaat)
                .ThenInclude(s => s.Circuit)
                .SingleOrDefaultAsync(s => s.Id == id);
        }
    }
}


