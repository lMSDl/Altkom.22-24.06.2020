using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    class StudentConfiguration : EntityTypeConfiguration<Student>
    {
        public StudentConfiguration()
        {
            Property(x => x.FirstName)
                .HasMaxLength(15)
                .IsRequired();

            Property(x => x.LastName)
                .HasMaxLength(15)
                .IsRequired();

            Ignore(x => x.SomeProperty);
            Ignore(x => x.Mentor);
        }
    }
}
