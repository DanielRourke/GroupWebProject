using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupWebProject.Models
{
    public class Booking
    {

        /// <summary>
        /// Primary key via naming conventions
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Foreign Key for Room
        /// </summary>
        public int RoomID { get; set; }

        /// <summary>
        /// Foreign Key For Customer
        /// </summary>
        public string CustomerEmail { get; set; }

        /// <summary>
        /// Date customer is to check in
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "Check In")]
        public DateTime CheckIn { get; set; }

        /// <summary>
        /// Date customer is to check out
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "Check Out")]
        public DateTime CheckOut { get; set; }
       
        /// <summary>
        /// Total Cost of booking
        /// </summary>
        [DataType(DataType.Currency)]
        public decimal Cost { get; set; }

        /// <summary>
        /// Each Booking has one Room
        /// </summary>
        public Room TheRoom { get; set; }
        
        /// <summary>
        /// Each Booking has one Customer
        /// </summary>
        public Customer TheCustomer { get; set; }
    }
}
