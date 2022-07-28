using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OrionTek.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Directions = new HashSet<Direction>();
        }

        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string NationalId { get; set; } = null!;
        public int Age { get; set; }

        [InverseProperty("Customer")]
        public virtual ICollection<Direction> Directions { get; set; }
    }
}
