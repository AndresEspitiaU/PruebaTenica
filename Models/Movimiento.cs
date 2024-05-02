using System;
using System.Collections.Generic;

namespace PruebaTecnica.Models;

public partial class Movimiento
{
    public int MovId { get; set; }

    public DateOnly? MovFecha { get; set; }

    public int? ProId { get; set; }

    public string MovTipo { get; set; } 
    public string MovCantidad { get; set; }
    public decimal MovValor { get; set; }

    public virtual Producto? Pro { get; set; }
}
