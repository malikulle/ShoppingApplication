using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Entities.Dtos
{
    public class CategoryAddDto
    {
        [Required(ErrorMessage = "Ad girilmek Zorundadır")]
        public string Name { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;
    }
}
