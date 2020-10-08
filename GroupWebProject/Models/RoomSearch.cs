using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupWebProject.Models
{
    public class RoomSearch
    {

        /// <summary>
        /// Number of Beds in a room
        /// </summary>
        /// 
        [Required]
        [Range(1, 3,
            ErrorMessage = "number of beds in the room; +" +
            "can only be 1, 2, or 3.")]
        public int BedCount { get; set; }
        /// <summary>
        /// Date customer is to check in
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Check In")]
        public DateTime CheckIn { get; set; }

        /// <summary>
        /// Date customer is to check out
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Check Out")]
        public DateTime CheckOut { get; set; }
    }
}
