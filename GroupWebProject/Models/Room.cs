using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupWebProject.Models
{
    public class Room
    {
        /// <summary>
        /// Primary Key for Room
        /// </summary>
        public int ID { get; set; }


        /// <summary>
        /// Level that room is on 
        /// </summary>
        [Required]
        [RegularExpression(@"^[G123]$", 
            ErrorMessage = "Meaning the level of this room+" +
            " Exactly one character of ‘G’, ‘1’, ‘2’, or ‘3’.")]
        public string Level { get; set; }


        /// <summary>
        /// Number of Beds in a room
        /// </summary>
        [Range(1, 3, 
            ErrorMessage = "number of beds in the room; +" +
            "can only be 1, 2, or 3.")]
        public int BedCount { get; set; }

        /// <summary>
        /// Price of Room Per Night
        /// </summary>
        [DataType(DataType.Currency)]
        [Range(50.00, 300.00, ErrorMessage = "price per night; +" +
            " Between $50 and $300.")]
        public decimal Price { get; set; }

        /// <summary>
        /// Collection of Bookings for this room
        /// </summary>
        public ICollection<Booking> TheBookings { get; set; }


    }
}
