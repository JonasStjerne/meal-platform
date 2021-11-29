using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Food_Like.Shared
{
    public class SetupSellerRequest

    {
        [Required]
        public Address Address { get; set; }
        [Required]
        public byte[] KitchenPicture { get; set; }
    }
}
