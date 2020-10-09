using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupWebProject.Models
{
    public class RoomBookingStatistic
    {
        [Display(Name = "Room ID")]
        public int Room { get; set; }
        [Display(Name = "Number of Bookings")]
        public int BookingCount { get; set; }

    }
}
