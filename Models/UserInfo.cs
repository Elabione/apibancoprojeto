using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;

namespace apiprojeto.Models;

public class UserInfo
{
    public string Email { get; set; } = null!;
    public string Password{get; set;}
    public decimal Cpf { get; set; }
}
