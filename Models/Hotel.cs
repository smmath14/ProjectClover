using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JodhpurPalace.Models
{
    public class Hotel
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public string RoomType { get; set; }
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int Price { get; set; }
        public int Available { get; set; }
        
        public string Images { get; set; }

    }
}
