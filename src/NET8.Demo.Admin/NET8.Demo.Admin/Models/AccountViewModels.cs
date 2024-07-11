using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NET8.Demo.Admin.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    //public class SendCodeViewModel
    //{
    //    public string SelectedProvider { get; set; }
    //    public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    //    public string ReturnUrl { get; set; }
    //    public bool RememberMe { get; set; }
    //}

    //public class VerifyCodeViewModel
    //{
    //    [Required]
    //    public string Provider { get; set; }

    //    [Required]
    //    [Display(Name = "Code")]
    //    public string Code { get; set; }

    //    public string ReturnUrl { get; set; }

    //    [Display(Name = "Remember this browser?")]
    //    public bool RememberBrowser { get; set; }

    //    public bool RememberMe { get; set; }
    //}

    public class LoginViewModel
    {
        [Required(ErrorMessage = "{0} bắt buộc nhập.")]
        [Display(Name = "Tài khoản")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} bắt buộc nhập.")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Display(Name = "Ghi nhớ mật khẩu?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "{0} bắt buộc nhập.")]
        [Display(Name = "Họ")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "{0} bắt buộc nhập.")]
        [Display(Name = "Tên")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "{0} bắt buộc nhập.")]
        [Display(Name = "Số điện thoại")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "{0} khôp hợp lệ.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "{0} bắt buộc nhập.")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} bắt buộc nhập.")]
        [StringLength(100, ErrorMessage = "{0} phải nhập ít nhất {2} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp.")]
        //RequireDigit = true,
        //RequireLowercase = true,
        //RequireUppercase = true,
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ email.")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ.")]
        [Display(Name = "Vui lòng nhập email để lấy lại mật khẩu.")]
        public string Email { get; set; }
    }
}
