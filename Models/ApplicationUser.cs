using Microsoft.AspNetCore.Identity;

namespace apiprojeto.Models;

public class ApplicationUser : IdentityUser
{
        public decimal Cpf { get; set; }
}
