

using AutoMapper.Configuration.Annotations;
using Newtonsoft.Json;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using ColumnAttribute = Supabase.Postgrest.Attributes.ColumnAttribute;
using TableAttribute = Supabase.Postgrest.Attributes.TableAttribute;

namespace MeuPontoOnline.Models
{
    [Table("Funcao")]
    public class Funcao : BaseModel
    {
        [PrimaryKey("Id")]
     
        public int Id { get; set; }

        [Column("nome")]
        public string Nome { get; set; } = string.Empty;

        [Column("descricao")]
        public string? Descricao { get; set; }


        [JsonIgnore]
        public ICollection<Funcionario>? Funcionarios { get; set; }
    }
}
