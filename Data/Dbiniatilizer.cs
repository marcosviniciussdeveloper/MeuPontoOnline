using MeuPontoOnline.Models;

namespace MeuPontoOnline.Data
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Setores.Any())
            {
                var setores = new List<Setor>
                {
                    new() { Nome = "RH" },
                    new() { Nome = "Financeiro" },
                    new() { Nome = "TI" },
                    new() { Nome = "Operacional" }
                };

                context.Setores.AddRange(setores);
                context.SaveChanges();
            }
        }
    }
}
