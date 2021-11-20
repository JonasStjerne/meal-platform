using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Food_Like.Shared
{
    public partial class Category
    {
        public Category()
        {
            Mealcategory = new HashSet<Mealcategory>();
        }

        public int CategoryId { get; set; }
        public string Titel { get; set; }

        public virtual ICollection<Mealcategory> Mealcategory { get; set; }
    }
}
