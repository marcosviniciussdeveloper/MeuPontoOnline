using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeuPontoOnline.Models
{
    [Table("setores")]
    public class Setor
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("nome")]
        public string Nome { get; set; } = string.Empty;

        public ICollection<Funcionario>? Funcionarios { get; set; }
    }
}
