using StudentManagement.Models;
using StudentManagement.Models.Interface;
using System.Collections.Generic;
using System.Linq;

namespace StudentManagement.DataRepo
{
    public class SQLStudentRepo : IStudentRepository
    {
        private readonly AppDbContext _context;

        public SQLStudentRepo(AppDbContext context)
        {
            _context = context;
        }

        public int StudentsCount => _context.Students.Count();

        public Student AddStudent(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
            return student;
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _context.Students;
        }

        public Student GetStudent(int id)
        {
            return _context.Students.Find(id);
        }

        public void RemoveStudent(int id)
        {
            Student student = _context.Students.Find(id);
            if(student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
        }

        public Student SaveStudent(Student student)
        {
            var s = _context.Students.Attach(student);
            s.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return student;

        }
    }
}
