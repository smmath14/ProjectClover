using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JodhpurPalace.Models
{
    public class GuestCalender
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CheckIn { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime CheckOut { get; set; }
        

        public int Guest { get; set; }
        

        public int Rooms { get; set; }
        public int No_of_Days { get; set; }

    }
}
