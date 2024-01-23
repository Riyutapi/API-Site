using CRUDAPI.Service.DepartamentoS;
using CRUDAPI.Service.FuncionarioS;
using ECore.WebAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace CRUDAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Adicionar serviços ao contêiner.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IDepartamentoInterface, DepartamentoService>();
            builder.Services.AddScoped<IFuncionarioInterface, FuncionarioService>();

            // Configurar o DbContext para usar o SQL Server.
            builder.Services.AddDbContext<Contexto>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Configurar CORS para permitir solicitações do seu aplicativo Angular
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            // Configurar o pipeline de solicitação HTTP.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Habilitar CORS
            app.UseCors();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            // Mapear a rota padrão do controlador.
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
