using StudentManagement.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    public class StudentRepository : IStudentRepository
    {
        public StudentRepository()
        {
            students = new List<Student>();
        }

        /// <summary>
        /// 学生数量
        /// </summary>
        public int StudentsCount => students.Count;

        public Student GetStudent(int id)
        {
            return students.FirstOrDefault(x => x.Id == id);
        }

        public Student SaveStudent(Student student)
        {
            Student stu = null;
            if (students.Contains(student))
            {
                stu = students.FirstOrDefault(x => x.Id == student.Id);
                stu.Major = student.Major;
                stu.Name = student.Name;
            }
            else
                students.Add(stu);
            return stu;
        }

        public Student AddStudent(Student student)
        {
            int id = 0;
            if (students.Count > 0)
            {
                id = students.Max(x => x.Id) + 1;
            }
            student.Id = id;
            students.Add(student);
            return student;
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return students;
        }

        /// <summary>
        /// 删除学生
        /// </summary>
        /// <param name="id"></param>
        public void RemoveStudent(int id)
        {
            var s = students.FirstOrDefault(x => x.Id == id);
            if(s != null)
            {
                students.Remove(s);
            }
        }

        private List<Student> students = null;
    }
}
