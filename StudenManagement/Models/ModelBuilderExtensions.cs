using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudenManagement.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = 1,
                    Name = "李江",
                    ClassName = ClassNameEnum.FirstGrade,
                    Email = "453202851@qq.com",
                },
                
                new Student
                {
                Id = 2,
                Name = "叶茜茜",
                ClassName = ClassNameEnum.ThirdGrade,
                Email = "yezi@qq.com",
                }

                );
        }
    }
}
