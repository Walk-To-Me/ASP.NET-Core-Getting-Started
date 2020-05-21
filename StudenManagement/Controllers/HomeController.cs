using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using StudenManagement.Models;
using StudenManagement.ViewModels;

namespace StudenManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public HomeController(IStudentRepository studentRepository, IWebHostEnvironment webHostEnvironment)
        {
            _studentRepository = studentRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var model = _studentRepository.GetAllStudents();
            return View(model);
        }

        public IActionResult Details(int id)
        {
           
            Student student = _studentRepository.GetStudent(id);

            if (student == null)
            {
                throw new Exception("此异常发生在Detail视图中");
                // Response.StatusCode = 404;
                // return View("StudentNotFound", id);
            }


            HomeDetailsViewModels homeDetailsViewModels = new HomeDetailsViewModels()
            {
                Student = student,
                PageTitle = "学生详细信息",
            };

            return View(homeDetailsViewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();

        }

        //[HttpPost]
        //public JsonResult Create(string data)
        //{
        //    JObject obj = JObject.Parse(data);

        //    string serial_number = obj["id"].ToString();

        //    // string level_1 = obj["entry"]["field_23"]["level_1"].ToString();

        //    return Json(serial_number);

        //}



        [HttpPost]
        public IActionResult Create(StudentCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;

                if (model.Photo != null)
                {
                    uniqueFileName = ProcessUploadFile(model);
                }
                Student newStudent = new Student
                {
                    Name = model.Name,
                    Email = model.Email,
                    ClassName = model.ClassName,
                    PhotoPath = uniqueFileName
                };


                _studentRepository.Add(newStudent);
                return RedirectToAction("Details", new { id = newStudent.Id });
            }

            return View();

        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Student student = _studentRepository.GetStudent(id);

            StudentEditViewModel studentEditView = new StudentEditViewModel
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                ClassName = student.ClassName,
                ExistingPhotoPath = student.PhotoPath
            };

            return View(studentEditView);
        }

        [HttpPost]
        public IActionResult Edit(StudentEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Student student = _studentRepository.GetStudent(model.Id);
                student.Email = model.Email;
                student.ClassName = model.ClassName;


                if (model.ExistingPhotoPath != null)
                {
                    string deleteFilePath = Path.Combine(webHostEnvironment.WebRootPath, "images", model.ExistingPhotoPath);

                    System.IO.File.Delete(deleteFilePath);

                }

                student.PhotoPath = ProcessUploadFile(model);


                Student updateStudent = _studentRepository.Update(student);

                return RedirectToAction("Index");
            }

            return View();
        }

        /// <summary>
        /// 将照片保存到指定的路径中，并返回唯一的文件名
        /// </summary>
        /// <returns></returns>
        private string ProcessUploadFile(StudentCreateViewModel model)
        {
            string uniqueFileName = null;

            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }

            }

            return uniqueFileName;
        }

       
    }
}