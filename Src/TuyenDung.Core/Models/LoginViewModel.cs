using System.ComponentModel.DataAnnotations;

namespace TuyenDung.Core.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "xin vui lòng nhập tài khoản !"), DataType(DataType.EmailAddress), MaxLength(20, ErrorMessage = "Tài khoản phải ít hơn 20 kí tự"), MinLength(5, ErrorMessage = "Mật khẩu phải nhiều hơn 4 kí tự")]
        public string AccountName { get; set; }

        [Required(ErrorMessage = "Xin vui lòng nhập mật khẩu !"), DataType(DataType.Password), MaxLength(20, ErrorMessage = "Mật khẩu phải ít hơn 20 kí tự"), MinLength(5, ErrorMessage = "Mật khẩu phải nhiều hơn 4 kí tự")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}