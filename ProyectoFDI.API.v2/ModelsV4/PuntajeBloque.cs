using System;
using System.Collections.Generic;

namespace ProyectoFDI.API.v2.ModelsV4;

public partial class PuntajeBloque
{
    public int IdBloPts { get; set; }

    public int? IdCom { get; set; }

    public int? IdDep { get; set; }

    public int? NumeroBloque { get; set; }

    public int? IntentosTops { get; set; }

    public int? IntentosZonas { get; set; }

    public string? Etapa { get; set; }

    public virtual Competencium? IdComNavigation { get; set; }

    public virtual Deportistum? IdDepNavigation { get; set; }
}
