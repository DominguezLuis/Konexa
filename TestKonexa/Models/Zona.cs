using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestKonexa.Models
{
    public class Zona
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreCorto { get; set; }
        public string Codigo { get; set; }
        public string Observacion { get; set; }

        [ForeignKey("ResponsableZonal")]
        public int IdResponsableZonal { get; set; }
        public ResponsableZonal ResponsableZonal { get; set; }
        public int IdSucursalCabecera { get; set; }

    }
}
