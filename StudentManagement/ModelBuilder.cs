using Microsoft.EntityFrameworkCore;
using StudentManagement.Models;

namespace StudentManagement
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(new Student
            {
                Name = "张三",
                Id = 1,
                Email = "123456@sina.com",
                Major = MajorEnum.Physics,
            }, new Student
            {
                Name = "李四",
                Id = 2,
                Email = "512351@gmail.com",
                Major = MajorEnum.English,
            }, new Student
            {
                Name = "赵六",
                Id = 3,
                Email = "zsix33333@yahoo.com",
                Major = MajorEnum.Chemistry,
            });
        }
    }
}
