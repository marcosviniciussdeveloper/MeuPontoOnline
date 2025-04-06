using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace MeuPontoOnline.Models
{
    [Table("registrarponto")]

    public class RegistroPonto : BaseModel
    {
        [PrimaryKey("id" , false)]
      
        public int? Id { get; set; }

        [Column("funcionario_id")]
        public int FuncionarioId { get; set; }

        [Column("tipo_registro")]
        public string TipoRegistro { get; set; } = string.Empty;

        [Column("data_hora")]
        public DateTime DataHora { get; set; } = DateTime.UtcNow;

        [Column("observacao")]
        public string? Observacao { get; set; } = string.Empty;
    }
}
