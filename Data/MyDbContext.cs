using APIProjeto.Data.Map;
using APIProjeto.Models;
using Microsoft.EntityFrameworkCore;

namespace APIProjeto.Data
{
    public class MyDbContext: DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
        public DbSet<Agendamento> Agendamentos { get; set; }
        public DbSet<Demanda> Demandas { get; set; }
        public DbSet<Especialidade> Especialidades { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<Procedimento> Procedimentos { get; set; }
        public DbSet<UnidadeSaude> UnidadesSaude { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Vaga> Vagas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AgendamentoMap());
            modelBuilder.ApplyConfiguration(new DemandaMap());
            modelBuilder.ApplyConfiguration(new EspecialidadeMap());
            modelBuilder.ApplyConfiguration(new PacienteMap());
            modelBuilder.ApplyConfiguration(new PerfilMap());
            modelBuilder.ApplyConfiguration(new ProcedimentoMap());
            modelBuilder.ApplyConfiguration(new UnidadeMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new VagaMap());


            // ================================
            // SEEDING — DADOS INICIAIS DO BANCO
            // ================================

            // ---- PERFIS ----
            modelBuilder.Entity<Perfil>().HasData(
                new Perfil { Id = 1, Nome = "Administrador" },
                new Perfil { Id = 2, Nome = "Regulador" },
                new Perfil { Id = 3, Nome = "Solicitante" }
            );

            // ---- ESPECIALIDADES ----
            modelBuilder.Entity<Especialidade>().HasData(
                new Especialidade { Id = 1, Nome = "Cardiologia" },
                new Especialidade { Id = 2, Nome = "Ortopedia" },
                new Especialidade { Id = 3, Nome = "Pediatria" }
            );

            // ---- UNIDADES DE SAÚDE ----
            modelBuilder.Entity<UnidadeSaude>().HasData(
                new UnidadeSaude { Id = 1, Nome = "UBS Central", Tipo = "UBS", Regiao = "Centro" },
                new UnidadeSaude { Id = 2, Nome = "UPA Norte", Tipo = "UPA", Regiao = "Zona Norte" }
            );

            // ---- USUÁRIOS ----
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = 1,
                    Nome = "Administrador",
                    Cpf = "00000000000",
                    Senha = "123456",
                    IdUnidadeSaude = 1,
                    IdPerfil = 1
                },
                new Usuario
                {
                    Id = 2,
                    Nome = "Regulador Teste",
                    Cpf = "11111111111",
                    Senha = "123456",
                    IdUnidadeSaude = 1,
                    IdPerfil = 2
                },
                new Usuario
                {
                    Id = 3,
                    Nome = "Solicitante Teste",
                    Cpf = "22222222222",
                    Senha = "123456",
                    IdUnidadeSaude = 2,
                    IdPerfil = 3
                }
            );

            // ---- PACIENTES ----
            modelBuilder.Entity<Paciente>().HasData(
                new Paciente
                {
                    Id = 1,
                    Nome = "João da Silva",
                    Cpf = "12345678900",
                    Cns = "998877665544",
                    DataNascimento = new DateTime(1990, 5, 20),
                    Endereco = "Rua A, 123"
                },
                new Paciente
                {
                    Id = 2,
                    Nome = "Maria Oliveira",
                    Cpf = "98765432100",
                    Cns = "112233445566",
                    DataNascimento = new DateTime(1985, 2, 15),
                    Endereco = "Rua B, 45"
                }
            );

            // ---- PROCEDIMENTOS ----
            modelBuilder.Entity<Procedimento>().HasData(
                new Procedimento
                {
                    Id = 1,
                    CodigoProcedimento = "PROC001",
                    NomeProcedimento = "Consulta Cardiologia",
                    Tipo = "Consulta",
                    IdEspecialidade = 1
                },
                new Procedimento
                {
                    Id = 2,
                    CodigoProcedimento = "PROC002",
                    NomeProcedimento = "Raio-X de Mão",
                    Tipo = "Exame",
                    IdEspecialidade = 2
                }
            );

            // ---- VAGAS ----
            modelBuilder.Entity<Vaga>().HasData(
                new Vaga
                {
                    Id = 1,
                    IdProcedimento = 1,
                    IdUnidadeOfertante = 1,
                    Quantidade = 10,
                    MesAnoReferencia = "01/2025"
                },
                new Vaga
                {
                    Id = 2,
                    IdProcedimento = 2,
                    IdUnidadeOfertante = 2,
                    Quantidade = 5,
                    MesAnoReferencia = "01/2025"
                }
            );

            // ---- DEMANDAS ----
            modelBuilder.Entity<Demanda>().HasData(
                new Demanda
                {
                    Id = 1,
                    IdPaciente = 1,
                    IdProcedimento = 1,
                    IdUnidadeSolicitante = 1,
                    IdUsuarioSolicitante = 3, // solicitante
                    DataSolicitacao = new DateTime(2025, 1, 10),
                    Prioridade = "Alta",
                    Status = "Aguardando",
                    Justificativa = "Paciente com dor no peito"
                }
            );

            // ---- AGENDAMENTOS ----
            modelBuilder.Entity<Agendamento>().HasData(
                new Agendamento
                {
                    Id = 1,
                    IdDemanda = 1,
                    IdVaga = 1,
                    IdUsuarioRegulador = 2, // regulador
                    DataAgendamento = new DateTime(2025, 1, 12),
                    DataRealizacao = new DateTime(2025, 1, 20),
                    StatusComparecimento = "Pendente"
                }
            );


            base.OnModelCreating(modelBuilder);
        }
    }
}
