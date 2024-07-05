using System.ComponentModel.DataAnnotations;

public class RegistrastionModel {
    [Required(ErrorMessage = "Tên không được trống!")]
    public string sName { get; set; }
    [Required(ErrorMessage = "Email không được trống!")]
    public string sEmail { get; set; }
    
    [RegularExpression(@"^\d.*$", ErrorMessage = "Ký tự đầu tiên phải là số")]
    public string sAddress { get; set; }
    [Required(ErrorMessage = "Mật khẩu không được trống!")]
    //[RegularExpression(@"^\d.*$", ErrorMessage = "Ký tự đầu tiên phải là số")]
    public string sPassword { get; set; }
    [Required(ErrorMessage = "Nhập lại mật khẩu")]
    [Compare("sPassword", ErrorMessage = "Hai mật khẩu phải giống nhau!")]
    public string sPasswordConfirm { get; set; }
}