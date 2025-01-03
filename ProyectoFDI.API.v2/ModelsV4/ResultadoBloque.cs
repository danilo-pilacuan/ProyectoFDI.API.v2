using System;
using System.Collections.Generic;

namespace ProyectoFDI.API.v2.ModelsV4;

public partial class ResultadoBloque
{
    public int IdResBloque { get; set; }

    public int? IdCom { get; set; }

    public int? IdDep { get; set; }

    public int? Puesto { get; set; }

    public string? Etapa { get; set; }

    public virtual Competencium? IdComNavigation { get; set; }

    public virtual Deportistum? IdDepNavigation { get; set; }
}
