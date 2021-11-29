using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Food_Like.Shared
{
    public partial class CreateReviewRequest
    {
 
        [Required]
        public sbyte Rating { get; set; }
        public string ReviewDescription { get; set; }
    }
}

