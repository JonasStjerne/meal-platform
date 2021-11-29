using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Food_Like.Shared
{
    public class CreateMealRequest
    {


        [Required]
        public string Titel { get; set; }
        [Required]
        public sbyte Portions { get; set; }
        [Required]
        public decimal PortionPrice { get; set; }
        [Required]
        public string MealDescription { get; set; }
        [Required]
        public string Ingridients { get; set; }
        [Required]
        public DateTime PickupFrom { get; set; }
        [Required]
        public DateTime PickupTo { get; set; }
        [Required]
        public byte[] MealPicture { get; set; }


    }
}
