using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestKonexa.Models
{
    public class ZonaSucursal
    {
        [Key]
        [ForeignKey("Zona")]
        public int IdZona { get; set; }
        public Zona Zona { get; set; }
        [Key]
        [ForeignKey("Sucursal")]
        public int IdSucursal { get; set; }
        public Sucursal Sucursal { get; set; }
    }
}
