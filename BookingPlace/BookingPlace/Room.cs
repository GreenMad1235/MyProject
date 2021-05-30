using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingPlace
{
    class Room
    {
        //public Class Class { get; set; }
        public int RoomID { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public DateTime LastTime { get; set; }
        public int Price { get; set; } = 1500;
        public bool Status { get; set; } = true;


    }
}
