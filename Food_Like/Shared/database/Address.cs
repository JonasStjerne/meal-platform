using System;
using System.Collections.Generic;


// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Food_Like.Shared
{
    public partial class Address
    {
        public Address()
        {
            Seller = new HashSet<Seller>();
        }

        public int AddressId { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }

        public virtual ICollection<Seller> Seller { get; set; }
    }
}
