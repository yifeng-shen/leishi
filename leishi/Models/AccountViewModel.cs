using System.ComponentModel.DataAnnotations;
using System.Security;

namespace leishi.Web.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须小于 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "再次输入密码")]
        [Compare("Password", ErrorMessage = "两次密码输入不一致，请重新输入。")]
        public string ConfirmPassword { get; set; }
    }
}
