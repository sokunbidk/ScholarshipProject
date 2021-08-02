using System;

namespace ScholarshipManagement.Data.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime Created { get; set; } = DateTime.Today;

        public DateTime? Modified { get; set; } = DateTime.Today;

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public byte[] RowVersion { get; set; } //Concurrency

    }
}
