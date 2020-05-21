using Microsoft.AspNetCore.Http;
using StudenManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudenManagement.ViewModels
{
    public class StudentCreateViewModel
    {

        [Display(Name = "姓名")]
        [Required(ErrorMessage = "请输入名字")]
        [MaxLength(10, ErrorMessage = "名字过长")]
        public string Name { get; set; }

        [Required(ErrorMessage = "班级不能为空")]
        [Display(Name = "班级信息")]
        public ClassNameEnum? ClassName { get; set; }

        [Display(Name = "邮箱地址")]
        [Required(ErrorMessage = "邮箱不能为空")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
            ErrorMessage = "邮箱格式不正确")]
        public string Email { get; set; }

        [Display(Name = "图片")]
        public IFormFile Photo{ get; set; }
    }
}
