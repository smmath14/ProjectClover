using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JodhpurPalace.Models
{
    public class PalaceRecord
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RoomNo { get; set; }
        public int PhoneNo { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yy}")]
        public DateTime CheckIn { get; set; }


        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yy}")]
        public DateTime CheckOut { get; set; }


        [DisplayFormat(DataFormatString = "{0:N0}")]
        public string Amount { get; set; }
        public int No_Of_Rooms { get; set; }
        public int Total_Person { get; set; }
        public string RoomName { get; set; }
        public int GuestId { get; set; }
    }
}
