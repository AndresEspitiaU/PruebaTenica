using System;
using System.Collections.Generic;

namespace PruebaTecnica.Models;

public partial class Producto
{
    public int ProId { get; set; }

    public string ProCodigo { get; set; } = null!;

    public string ProNombre { get; set; } = null!;

    public string ProDescripcion { get; set; } = null!;

    public string ProDetalle { get; set; } = null!;

    public string ProPresentacion { get; set; } = null!;

    public string ProMarca { get; set; } = null!;

    public string ProProveedor { get; set; } = null!;

    public decimal ProCostocompra { get; set; }

    public decimal ProCostoventa1 { get; set; }

    public decimal ProCostoventa2 { get; set; }

    public string ProCantidad { get; set; } 

    public string ProCanmax { get; set; } = null!;

    public string ProCanmini { get; set; } = null!;

    public bool? ProActivo { get; set; }

    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();
}
