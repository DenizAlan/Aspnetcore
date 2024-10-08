using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Models
{
    public class RegisterViewModel
    {

        [Required]
        [Display(Name ="username")]
        public string? UserName { get; set; }

        [Required]
        [Display(Name ="Ad Soyad")]
        public string? Name { get; set; }
        
        [Required]
        [EmailAddress]
        [Display(Name ="Eposta")]
        public string? Email{get ; set ;}

        [Required]
        [StringLength(10 , ErrorMessage ="{0} alanı en az {2} karakter uzunluğunda olmalı" , MinimumLength=6)]
        [DataType(DataType.Password)]
        [Display(Name ="Parola")]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password) , ErrorMessage ="Parolalar eşleşmiyor")]
        [Display(Name ="Parola Tekrar")]
        public string? ConfirmPassword { get; set; }

    }
}


//[Compare(nameof(Password))]!Şifre doğrulama işlemi için oldukça kullanışlıdır!Bu özelliği kullanarak, yeni şifre ve şifre onay alanlarını karşılaştırabilir ve eşleşip eşleşmediğini kontrol edebilirsiniz. İşte nasıl kullanacağınız: