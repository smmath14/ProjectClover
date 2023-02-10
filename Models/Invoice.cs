using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JodhpurPalace.Models
{
    public class Invoice
    {
        public int GuestId { get; set; }
        public int InvoiceNo { get; set; }
        public DateTime DateOfBooking { get; set; }
        public string RoomName { get; set; }
        public string RoomType { get; set; }
        public int Amount { get; set; }
       
    }
}
