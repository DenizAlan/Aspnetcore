using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApp.ViewModels
{
    public class EditViewModel
    {
        public string? Id {get ; set ;}
        public string? FullName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password" , ErrorMessage ="Parolalar Eşleşmiyor")]
        public string? ConfirmPassword { get; set; }


        // Ilist List arayüzü
        public IList<string>? SelectedRoles {get ; set ;}
    }
}

     
