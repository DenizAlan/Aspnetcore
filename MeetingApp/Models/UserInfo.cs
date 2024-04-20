using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingApp.Models
{
    public class UserInfo
    {
        public int Id { get; set; }
        [Required (ErrorMessage ="Ad alanı zorunludur")]
        public string? Name { get; set; }
        [Required(ErrorMessage ="Telefon alanı zorunludur")]
        public string? Phone { get; set; }
        [Required]
        [EmailAddress(ErrorMessage ="Hatalı email")]
        public string? Email { get; set; }
        [Required(ErrorMessage ="Katılım Durumunu belirtiniz")]
        public bool? WillAttend { get; set; }

    }
}