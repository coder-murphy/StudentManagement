using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Models
{
    /// <summary>
    /// 学生信息
    /// </summary>
    public class StudentInfoViewModel
    {
        public int Id { get; set; }
        public string PageTitle { get; set; }

        [Display(Name = "名称")]
        [Required(ErrorMessage = "名称不能为空！")]
        public string Name { get; set; }

        [Display(Name = "邮箱")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
            ErrorMessage = "邮箱的格式不正确！")]
        [Required(ErrorMessage = "邮箱不能为空！")]
        public string Email { get; set; }

        [Display(Name = "主修科目")]
        [Required(ErrorMessage = "请选择一个科目!")]
        public MajorEnum? Major { get; set; }


        public IFormFile Icon { get; set; }
    }
}
