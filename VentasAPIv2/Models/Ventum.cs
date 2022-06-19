using System;
using System.Collections.Generic;

namespace VentasAPIv2.Models
{
    public partial class Ventum
    {
        public Ventum()
        {
            ConceptosVenta = new HashSet<ConceptosVentum>();
        }

        public string Id { get; set; } = null!;
        public string Fecha { get; set; } = null!;
        public decimal Total { get; set; }
        public string Idcliente { get; set; } = null!;
        public decimal Montocambio { get; set; }
        public decimal Montopago { get; set; }

        public virtual ICollection<ConceptosVentum> ConceptosVenta { get; set; }
    }
}
