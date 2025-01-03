using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFDI.API.v2.ModelsV4;

public partial class VistaVeloClasificacion
{
    [Key]
    public int IdCompe { get; set; }

    public int? Puesto { get; set; }

    public string? ResultadoClasificacion { get; set; }

    public string Deportista { get; set; } = null!;
}
