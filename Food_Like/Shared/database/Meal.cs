using System;
using System.Collections.Generic;

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
        public DateTime CreatedAt { get; set; }
        public byte[] MealPicture { get; set; }

        public virtual Seller Seller { get; set; }
        public virtual ICollection<Mealcategory> Mealcategory { get; set; }
        public virtual ICollection<Mealorder> Mealorder { get; set; }
        public virtual ICollection<Review> Review { get; set; }
    }
}
