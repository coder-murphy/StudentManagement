using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public MajorEnum? Major { get; set; }

        public string Email { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Icon { get; set; }
    }
}
