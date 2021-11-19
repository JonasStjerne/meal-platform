using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Food_Like.Shared
{
    public partial class Mealorder
    {
        public int OrderId { get; set; }
        public int BuyerId { get; set; }
        public int MealId { get; set; }
        public DateTime PickUpTime { get; set; }
        public sbyte Quantity { get; set; }

        public virtual Buyer Buyer { get; set; }
        public virtual Meal Meal { get; set; }
    }
}
