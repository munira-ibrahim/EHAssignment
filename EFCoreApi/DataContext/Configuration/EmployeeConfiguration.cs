using EFCoreApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EFCoreApi.Data.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employees>
    {
        public void Configure(EntityTypeBuilder<Employees> builder)
        {
            builder.ToTable("Employees");
            builder.Property(s => s.Age)
                .IsRequired(false);

            builder.HasData
            (
                new Employees()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Jhon",
                    LastName = "doe",
                    EmailAddress = "JhonDoe@gmail.com",
                    Age = 25
                },
                new Employees()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Jacob",
                    LastName = "Hilderth",
                    EmailAddress = "JacobHilderth@gmail.com",
                    Age = 30
                },
                new Employees()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Jacob",
                    LastName = "Hilderth",
                    EmailAddress = "JacobHilderth@gmail.com",
                    Age = 30
                },
                new Employees()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Alexandra",
                    LastName = "William",
                    EmailAddress = "AlexWilliam@gmail.com",
                    Age = 22
                },
                new Employees()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Jonita",
                    LastName = "Peter",
                    EmailAddress = "JonitaPeter@gmail.com",
                    Age = 27
                }
            );
        }
    }
}
