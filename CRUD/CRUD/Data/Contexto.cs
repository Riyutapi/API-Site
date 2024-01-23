using ECore.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ECore.WebAPI.Data
{
    // Classe de contexto que herda de DbContext do Entity Framework Core.
    public class Contexto : Microsoft.EntityFrameworkCore.DbContext
    {
        // Construtor que recebe as opções de contexto ao ser instanciado.
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
            // As opções do contexto são configuradas no construtor da classe pai (base).
        }

        // Define um DbSet para a entidade Departamento, que representa a tabela no banco de dados.
        public DbSet<Departamento> Departamentos { get; set; }

        // Define um DbSet para a entidade Funcionario, que representa a tabela no banco de dados.
        public DbSet<Funcionario> Funcionarios { get; set; }

        // Método chamado durante a configuração do contexto para definir as opções de conexão.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configura o provedor de banco de dados e a cadeia de conexão.
            optionsBuilder.UseSqlServer("Integrated Security=SSPI;TrustServerCertificate=True;Persist Security Info=False;Initial Catalog=Riyu;Data Source=localhost\\SQLEXPRESS");
        }
    }
}
