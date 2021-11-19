using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Food_Like.Shared
{
    public partial class Buyer
    {
        public Buyer()
        {
            Mealorder = new HashSet<Mealorder>();
            Review = new HashSet<Review>();
        }

        public int BuyerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] ProfilePicture { get; set; }
        public string EncryptedPassword { get; set; }

        public virtual Seller Seller { get; set; }
        public virtual ICollection<Mealorder> Mealorder { get; set; }
        public virtual ICollection<Review> Review { get; set; }
    }
}
