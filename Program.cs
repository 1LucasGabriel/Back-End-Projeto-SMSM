using APIProjeto.Data; // Troque pelo namespace do seu DbContext
using APIProjeto.Repositories;
using APIProjeto.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace APIProjeto
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // ---------------- POSTGRES CONNECTION ----------------
            builder.Services.AddDbContext<MyDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddScoped<IPerfilRepository, PerfilRepository>();
            builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
            builder.Services.AddScoped<IUnidadeSaudeRepository, UnidadeSaudeRepository>();
            builder.Services.AddScoped<IProcedimentoRepository, ProcedimentoRepository>();
            builder.Services.AddScoped<IEspecialidadeRepository, EspecialidadeRepository>();
            builder.Services.AddScoped<IDemandaRepository, DemandaRepository>();
            builder.Services.AddScoped<IVagaRepository, VagaRepository>();
            builder.Services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();


            var app = builder.Build();

            // ---------------- CRIAR TABELAS AUTOMATICAMENTE ----------------
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<MyDbContext>();
                db.Database.Migrate(); // aplica migrations e cria tabelas
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
