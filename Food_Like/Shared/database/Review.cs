using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Food_Like.Shared
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public int BuyerId { get; set; }
        public int MealId { get; set; }
        public sbyte Rating { get; set; }
        public string ReviewDescription { get; set; }

        public virtual Buyer Buyer { get; set; }
        public virtual Meal Meal { get; set; }
    }
}
