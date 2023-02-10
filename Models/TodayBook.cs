using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JodhpurPalace.Models
{
    public class TodayBook
    {
        public string Username { get; set; }
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public DateTime ReserveIn { get; set; }
        public DateTime ReserveTo { get; set; }
    }
}
