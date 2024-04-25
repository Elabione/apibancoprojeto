using System;
using System.Collections.Generic;

namespace apiprojeto.Models;

public partial class Epi
{
    public int IdEpi { get; set; }

    public string InsUso { get; set; } = null!;

    public string NomeEpi { get; set; } = null!;

    public int Qtd { get; set; }

    public virtual ICollection<Entrega> Entregas { get; } = new List<Entrega>();
}
