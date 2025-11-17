using APIProjeto.Data;
using APIProjeto.Repositories;
using APIProjeto.Repositories.Interfaces;
using APIProjeto.Services;
using APIProjeto.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace APIProjeto
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ---------------- ADD SERVICES ----------------
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // ---------------- POSTGRES CONNECTION ----------------
            builder.Services.AddDbContext<MyDbContext>(options =>
                options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
                ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
                ));

            // ---------------- REPOSITORIES ----------------
            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddScoped<IPerfilRepository, PerfilRepository>();
            builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
            builder.Services.AddScoped<IUnidadeSaudeRepository, UnidadeSaudeRepository>();
            builder.Services.AddScoped<IProcedimentoRepository, ProcedimentoRepository>();
            builder.Services.AddScoped<IEspecialidadeRepository, EspecialidadeRepository>();
            builder.Services.AddScoped<IDemandaRepository, DemandaRepository>();
            builder.Services.AddScoped<IVagaRepository, VagaRepository>();
            builder.Services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();

            // ---------------- TOKEN SERVICE ----------------
            builder.Services.AddScoped<ITokenService, TokenService>();

            // ---------------- CORS ----------------
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200") // porta do seu Angular
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });

            // ---------------- JWT AUTHENTICATION ----------------
            var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            var app = builder.Build();

            // ---------------- CRIAR TABELAS AUTOMATICAMENTE ----------------
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<MyDbContext>();
                db.Database.Migrate();
            }

            // ---------------- MIDDLEWARE ----------------
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(MyAllowSpecificOrigins); // <-- Habilita CORS
            app.UseAuthentication();            // <-- JWT Authentication
            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}
