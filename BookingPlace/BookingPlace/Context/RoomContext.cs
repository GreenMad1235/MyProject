using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BookingPlace.Context
{
    class RoomContext : DbContext
    {
        public DbSet <Room> Rooms { get; set; }
    }
}
