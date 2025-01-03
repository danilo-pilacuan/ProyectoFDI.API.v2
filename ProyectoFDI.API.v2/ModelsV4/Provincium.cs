using System;
using System.Collections.Generic;

namespace ProyectoFDI.API.v2.ModelsV4;

public partial class Provincium
{
    public int IdPro { get; set; }

    public string? NombrePro { get; set; }

    public virtual ICollection<Deportistum> Deportista { get; } = new List<Deportistum>();

    public virtual ICollection<Entrenador> Entrenadors { get; } = new List<Entrenador>();

    public virtual ICollection<Juez> Juezs { get; } = new List<Juez>();
}
