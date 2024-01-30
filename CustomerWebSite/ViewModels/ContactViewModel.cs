using System.ComponentModel.DataAnnotations;

namespace CustomerWebSite.ViewModels
{
    public class ContactViewModel
    {
        [Key] //要用內建的工具為此Model創建Controller的話需加上此

        [Required(ErrorMessage ="姓名欄位未填寫")] //[Required]為欄位必填，(ErrorMessage ="姓名欄位未填寫")為預設警告訊息
        [StringLength(maximumLength:8,MinimumLength =5,ErrorMessage ="姓名長度不正確")]
        [Display (Name ="姓名")]
        public string? Name { get; set; }


        [Required(ErrorMessage = "電子郵件欄位未填寫")]
        [EmailAddress(ErrorMessage ="電子郵件格式不正確")]
        [Display(Name = "電子郵件")]
        public string? Email { get; set; }

        [Display(Name = "連絡電話")]
        public string? Phone { get; set; }
    }
}