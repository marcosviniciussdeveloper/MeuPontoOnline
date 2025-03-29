namespace MeuPontoOnline.Data;
using MeuPontoOnline.Models;
using Microsoft.EntityFrameworkCore;


    public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Funcionario> Funcionarios { get; set; } = null!;

    public DbSet<Funcao> funcoes { get; set; }   
    public DbSet<Setor> Setores { get; set; } = null!;
    public DbSet<RegistroPonto> RegistroPontos { get; set; } = null!;
}

