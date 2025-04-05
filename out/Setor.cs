using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;
namespace MeuPontoOnline.Models;
using Newtonsoft.Json;

[Table("setores")]
public class Setor : BaseModel
{
    [PrimaryKey("id")]

    public int? Id { get; set; }

    [Column("nome")]
    public string Nome { get; set; } = string.Empty;

    [Column("descricao")]
    public string? Descricao { get; set; }


    [JsonIgnore]
    public ICollection<Funcionario>? Collection { get; set; }


}
