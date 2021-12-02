using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Food_Like.Shared
{
    public partial class Meal
    {
        public Meal()
        {
            Mealcategory = new HashSet<Mealcategory>();
            Mealorder = new HashSet<Mealorder>();
            Review = new HashSet<Review>();
        }

        public int MealId { get; set; }
        public int SellerId { get; set; }
        public string Titel { get; set; }
        public sbyte Portions { get; set; }
        public decimal PortionPrice { get; set; }
        public string MealDescription { get; set; }
        public string Ingridients { get; set; }
        public DateTime PickupFrom { get; set; }
        public DateTime PickupTo { get; set; }
        public byte[] MealPicture { get; set; }

        public virtual Seller Seller { get; set; }
        public virtual ICollection<Mealcategory> Mealcategory { get; set; }
        public virtual ICollection<Mealorder> Mealorder { get; set; }
        public virtual ICollection<Review> Review { get; set; }

        [NotMapped]
        public int Reserved { get; set; }

        [NotMapped]
        public decimal Rating { get; set; } = -1;

        [NotMapped]
        public dynamic Distance { get; set; }
    }
}
