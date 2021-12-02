using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Food_Like.Shared
{
    public partial class Seller
    {
        public Seller()
        {
            Meal = new HashSet<Meal>();
        }

        public int SellerId { get; set; }
        public int AddressId { get; set; }
        public byte[] KitchenPicture { get; set; }

        public virtual Address Address { get; set; }
        public virtual Buyer SellerNavigation { get; set; }
        public virtual ICollection<Meal> Meal { get; set; }

        

    }
}
