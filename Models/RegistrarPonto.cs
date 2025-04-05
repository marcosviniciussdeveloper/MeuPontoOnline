using MeuPontoOnline.Models;
using System.ComponentModel.DataAnnotations.Schema;
using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;
using ColumnAttribute = Supabase.Postgrest.Attributes.ColumnAttribute;
using TableAttribute = System.ComponentModel.DataAnnotations.Schema.TableAttribute;
using Newtonsoft.Json;

[Table("registro_ponto")]
public class RegistroPonto : BaseModel
{
    [PrimaryKey("id")]
    
    public int Id { get; set; }

    [ForeignKey("Funcionario")]
    [Column("funcionario_id")]
    public int FuncionarioId { get; set; }

    [Column("data_hora")]
    public DateTime DataHora { get; set; }

    [Column("tipo_registro")]
    public string? TipoRegistro { get; set; }

    [Column("observacao")]
    public string? Observacao { get; set; }
    
   // [JsonIgnore]
   // public Funcionario Funcionario { get; set; } = null!;
}//
