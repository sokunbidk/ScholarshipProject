﻿using ScholarshipManagement.Data.DTOs;
using ScholarshipManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace ScholarshipManagement.Data.Interfaces
{
    public interface IJamaatRepository:  IRepository<Jamaat>
    {
        //Task<IList<JamaatDto>> GetJamaatsAsync();
        Task<Jamaat> GetJamaatAsync(int circuitID);

        Task<Jamaat> GetJamaat(int id);

    }
}