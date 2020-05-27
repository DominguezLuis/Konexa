using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestKonexa.Models
{
    public class ZonaCrear
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreCorto { get; set; }
        public string Codigo { get; set; }
        public string Observacion { get; set; }
        public int ResponsableZonal { get; set; }
        public List<Sucursal> SucursalesDisponible { get; set; }
        public List<int> SucursalesSeleccionada { get; set; }
        public int Cabecera { get; set; }
        public string DesCabecera { get; set; }

    }
}
