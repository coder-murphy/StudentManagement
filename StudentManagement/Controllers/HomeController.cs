using Microsoft.AspNetCore.Mvc;
using StudentManagement.Models;
using StudentManagement.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Controllers
{
    /// <summary>
    /// 主控制器
    /// </summary>
    [Route("")]
    [Route("home")]
    public class HomeController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        public HomeController(IStudentRepository studentRepo)
        {
            studentRepository = studentRepo;
        }

        private readonly IStudentRepository studentRepository;

        /// <summary>
        /// 主页面
        /// </summary>
        /// <returns></returns>
        public string Index()
        {
            //return studentRepository.GetStudent(1).Name;
            return "Home Page";
        }

        // 属性路由
        [Route("info/{id}")] 
        public ViewResult Info(int id)
        {
            var model = studentRepository.GetStudent(id);
            StudentInfoViewModel studentInfoViewModel = new StudentInfoViewModel
            {
                PageTitle = "查询到的学生信息",
                Id = model.Id,
                Name = model.Name,
                Email = model.Email,
                Major = model.Major
            };
            ViewData["Title"] = "查询到的学生信息";
            return View(studentInfoViewModel);
        }

        /// <summary>
        /// 新建学生
        /// </summary>
        /// <returns></returns>
        [Route("create")]
        [HttpGet]
        public IActionResult CreateNew()
        {
            return View();
        }

        /// <summary>
        /// 新建学生
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [Route("create")]
        [HttpPost]
        public RedirectToActionResult CreateNew(Student student)
        {
            Student s = studentRepository.AddStudent(student);
            return RedirectToAction("create", new { id = s.Id });
        }

        [Route("allinfos")]
        public ViewResult Details()
        {
            var model = studentRepository.GetAllStudents();
            ViewData["PageTitle"] = "Student Details";
            return View(model);
        }
    }
}
