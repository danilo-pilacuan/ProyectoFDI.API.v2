using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFDI.API.v2.ModelsV4;

public partial class VistaViasResultado
{
    [Key]
    public int IdCompe { get; set; }

    public int? PuestoFinal { get; set; }

    public int? PuestoClasificacion { get; set; }

    public string Deportista { get; set; } = null!;

    public string? Clasificacion1 { get; set; }

    public string? Clasificacion2 { get; set; }

    public string? Final { get; set; }

    public TimeSpan? Tiempo { get; set; }
}
