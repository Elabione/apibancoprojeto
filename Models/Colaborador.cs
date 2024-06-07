using System;
using System.Collections.Generic;

namespace apiprojeto.Models;

public partial class Colaborador
{
    public int IdCol { get; set; }

    public string NomeCol { get; set; } = null!;

    public int Ctps { get; set; }

    public int Telefone { get; set; }

    public decimal Cpf { get; set; }

    public string Email { get; set; } = null!;

    public DateOnly DataAdmisão { get; set; }

    public virtual ICollection<Entrega> Entregas { get; } = new List<Entrega>();
}
