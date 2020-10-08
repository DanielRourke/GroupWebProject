using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupWebProject.Models
{
    public class RoomBooking
    {
        /// <summary>
        /// Foreign Key for Room
        /// </summary>
        [Required]
        public int RoomID { get; set; }

        /// <summary>
        /// Date customer is to check in
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "Check In")]
        [Required]
        public DateTime CheckIn { get; set; }

        /// <summary>
        /// Date customer is to check out
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "Check Out")]
        [Required]
        public DateTime CheckOut { get; set; }
    }
}
