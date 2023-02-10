using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JodhpurPalace.Models
{
    public class NewUser
    {
        
        [Required(ErrorMessage = "Please Enter Your UserId")]
        [Display(Name = "Username : ")]
       
        public string Username { get; set; }
       
        [Required(ErrorMessage = "Please Enter Your Password")]
        [Display(Name = "Password: ")]
        public string Password { get; set; }
        
        
        
        public string Confirm_Password { get; set; }
    }
}
