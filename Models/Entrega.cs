using System;
using System.Collections.Generic;

namespace apiprojeto.Models;

public partial class Entrega
{
    public int IdEnt { get; set; }

    public DateOnly DataValidade { get; set; }

    public DateOnly DateEntrega { get; set; }

    public int IdEpi { get; set; }

    public int IdCol { get; set; }

    public virtual Colaborador IdColNavigation { get; set; } = null!;

    public virtual Epi IdEpiNavigation { get; set; } = null!;
}
