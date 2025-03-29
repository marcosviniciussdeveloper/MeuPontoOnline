using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeuPontoOnline.Models
{

    [Table("registro_ponto")]
    public class RegistroPonto
    {
      [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("funcioario_id")] // Chave estrangeira
        public int FuncionarioId { get; set; }

        [Column("data_hora")]
        public DateTime DataHora { get; set; }

        [Column("tipo_registro")]
        public string? TipoRegistro { get; set; }

       [Column("Observacao")]
        public string? Observacao { get; set; }

        public Funcionario Fucionario { get; set; } = null!; // Propriedade de navegação



    }
}
