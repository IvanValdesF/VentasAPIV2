using System;
using System.Collections.Generic;

namespace VentasAPIv2.Models
{
    public partial class Compra
    {
        public Compra()
        {
            ConceptosCompras = new HashSet<ConceptosCompra>();
        }

        public string Id { get; set; } = null!;
        public string Fecha { get; set; } = null!;
        public decimal Total { get; set; }

        public virtual ICollection<ConceptosCompra> ConceptosCompras { get; set; }
    }
}
