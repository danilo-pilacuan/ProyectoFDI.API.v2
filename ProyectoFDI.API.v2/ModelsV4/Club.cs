using System;
using System.Collections.Generic;

namespace ProyectoFDI.API.v2.ModelsV4;

public partial class Club
{
    public int IdClub { get; set; }

    public string? NombreClub { get; set; }

    public virtual ICollection<Deportistum> Deportista { get; } = new List<Deportistum>();
}
