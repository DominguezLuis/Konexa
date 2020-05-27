using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestKonexa.Models
{
    public class ResponsableZonal
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public ICollection<Zona> Zona { get; set; }
    }
}
