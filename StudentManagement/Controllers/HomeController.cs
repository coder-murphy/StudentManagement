using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Models;
using StudentManagement.Models.Interface;
using System;
using System.Collections.Generic;
using System.IO;
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
        public HomeController(IWebHostEnvironment webHostEnvironment,IStudentRepository studentRepo)
        {
            studentRepository = studentRepo;
            _webHostEnvironment = webHostEnvironment;
        }

        private readonly IStudentRepository studentRepository;

        /// <summary>
        /// 主页面
        /// </summary>
        /// <returns></returns>
        public ViewResult Index()
        {
            //return studentRepository.GetStudent(1).Name;
            ImageCollectionViewModel imageCollectionViewModel = new ImageCollectionViewModel();
            return View(imageCollectionViewModel);
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
                Major = model.Major,
                Student = model
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
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("create")]
        [HttpPost]
        public IActionResult CreateNew(StudentInfoViewModel model)
        {
            if(ModelState.IsValid)
            {
                string uName = null;
                if(model.Icon != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    uName = Guid.NewGuid().ToString() + "_" + model.Icon.FileName;
                    string filePath = Path.Combine(uploadsFolder, uName);
                    model.Icon.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                Student student = new Student
                {
                    Name = model.Name,
                    Email = model.Email,
                    Major = model.Major,
                    Icon = uName
                }; 
                studentRepository.AddStudent(student);
                return RedirectToAction("info", new { id = student.Id });
            }
            return View();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("edit")]
        public ViewResult Edit(int id)
        {
            Student student = studentRepository.GetStudent(id);
            StudentEditViewModel studentEditViewModel = new StudentEditViewModel
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Major = student.Major,
                ExistingIconPath = student.Icon
            };
            return View(studentEditViewModel);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="studentEditViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit(StudentEditViewModel studentEditViewModel)
        {
            if(ModelState .IsValid)
            {
                Student student = studentRepository.GetStudent(studentEditViewModel.Id);
                studentEditViewModel.Name = student.Name;
                studentEditViewModel.Email = student.Email;
                studentEditViewModel.Major = student.Major;
                if(studentEditViewModel.Icon != null)
                {
                    if(studentEditViewModel.ExistingIconPath != null)
                    {
                        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", "avatars", studentEditViewModel.ExistingIconPath);
                        System.IO.File.Delete(path);
                    }
                    student.Icon = ProcessUploadedFile(studentEditViewModel);
                }
                Student updated = studentRepository.SaveStudent(student);
                return RedirectToAction("index");
            }
            return View(studentEditViewModel);
        }

        [Route("allinfos")]
        public ViewResult Details()
        {
            var model = studentRepository.GetAllStudents();
            ViewData["PageTitle"] = "Student Details";
            return View(model);
        }

        private string ProcessUploadedFile(StudentInfoViewModel model)
        {
            string uniqueFileName = null;
            if(model.Icon != null)
            {
                string upLoadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "avatars");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Icon.FileName;
                string filePath = Path.Combine(upLoadFolder, uniqueFileName);
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    model.Icon.CopyTo(fs);
                }
            }
            return uniqueFileName;
        }

        private IWebHostEnvironment _webHostEnvironment;
    }
}
