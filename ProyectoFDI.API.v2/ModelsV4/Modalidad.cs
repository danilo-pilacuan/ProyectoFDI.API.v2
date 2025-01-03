using System;
using System.Collections.Generic;

namespace ProyectoFDI.API.v2.ModelsV4;

public partial class Modalidad
{
    public int IdMod { get; set; }

    public string? DescripcionMod { get; set; }

    public virtual ICollection<Competencium> Competencia { get; } = new List<Competencium>();

    public virtual ICollection<DeportistaModalidad> DeportistaModalidads { get; } = new List<DeportistaModalidad>();
}
