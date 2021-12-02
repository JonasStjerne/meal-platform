using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Food_Like.Shared
{
    public partial class Meal
    {
        [NotMapped]
        public int Reserved { get; set; }
    }
}
