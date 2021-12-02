using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Food_Like.Shared
{
    public partial class Seller
    {
        [NotMapped]
        public decimal Rating { get; set; }
    }
}
