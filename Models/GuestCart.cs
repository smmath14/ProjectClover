using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JodhpurPalace.Models
{
    public class GuestCart
    {
        public int GuestId { get; set; }
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public string RoomType { get; set; }
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int Price { get; set; }
        public int Rooms { get; set; }
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int Total { get; set; }
        public int No_of_Days { get; set; }
        public int ItemId { get; set; }
    }
}
