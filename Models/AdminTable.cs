using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JodhpurPalace.Models
{
    public class AdminTable
    {
        //public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Your UserId")]
        [Display(Name = "Username : ")]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "Please Enter Your Password")]
        
        public string Password { get; set; }
       
        [Required(ErrorMessage = "Please Enter Your Password")]
       
        public string Confirm_Password { get; set; }
    }
}
