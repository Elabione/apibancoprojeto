using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace apiprojeto.Models;

public partial class Entrega
{
    public int IdEnt { get; set; }

    public DateOnly DataValidade { get; set; }

    public DateOnly DateEntrega { get; set; }

    public int IdEpi { get; set; }

    public int IdCol { get; set; }
    [JsonIgnore]
    public virtual Colaborador? IdColNavigation { get; set; }
    [JsonIgnore]
    public virtual Epi? IdEpiNavigation { get; set; }
}
