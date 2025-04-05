
using Supabase.Postgrest.Attributes;
using ColumnAttribute = Supabase.Postgrest.Attributes.ColumnAttribute;
using TableAttribute = Supabase.Postgrest.Attributes.TableAttribute;

namespace MeuPontoOnline.Models
{
    [Table("setores")]
    public class Setores
    {
        [PrimaryKey("id")] 
        public int? Id { get; set; }

        [Column("nome")]
        public string Nome { get; set; } = string.Empty;



        public ICollection<Funcionario>? Funcionarios { get; set; }
    }
}
