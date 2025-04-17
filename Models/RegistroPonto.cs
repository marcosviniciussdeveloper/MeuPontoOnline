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

        [Column("latitude")]
        public double? Latitude { get; set; }

        [Column("longitude")]
        public double? Longitude { get; set; }

        [Column("endereco_completo")]
        public string EnderecoCompleto { get; set; } 

        [Column("observacao")]
        public string? Observacao { get; set; } = string.Empty;
    }
}
