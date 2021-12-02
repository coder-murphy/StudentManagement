using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models.Interface
{
    public interface IStudentRepository
    {
        int StudentsCount { get; }

        Student GetStudent(int id);

        IEnumerable<Student> GetAllStudents();

        Student AddStudent(Student student);

        Student SaveStudent(Student student);

        void RemoveStudent(int id);
    }
}
