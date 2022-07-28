using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OrionTek.Models
{
    [Index("CustomerId", Name = "IX_Directions_CustomerId")]
    public partial class Direction
    {
        [Key]
        public Guid Id { get; set; }
        public string Calle { get; set; } = null!;
        public string Sector { get; set; } = null!;
        public string Ciudad { get; set; } = null!;
        public string Provincia { get; set; } = null!;
        public string Pais { get; set; } = null!;
        public Guid CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        [InverseProperty("Directions")]
        public virtual Customer Customer { get; set; } = null!;
    }
}
