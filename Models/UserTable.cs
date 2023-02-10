using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JodhpurPalace.Models
{
    public class User
    {
        public int GuestId { get; set; }
        [Required(ErrorMessage ="Please Enter Your UserId")]
       [Display(Name ="Username : ")]
        public string Username { get; set; }
      
        [Required(ErrorMessage ="Please Enter Your Password")]
        [DataType(DataType.Password)]

        public string Password { get; set; }
    }
}
