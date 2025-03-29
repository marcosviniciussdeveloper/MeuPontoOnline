using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeuPontoOnline.Models
{
    [Table("funcoes")]
    public class Funcao
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("nome")]
        public string Nome { get; set; } = string.Empty;

        [Column("descricao")]
        public string? Descricao { get; set; }

        public ICollection<Funcionario>? Funcionarios { get; set; }
    }
}
