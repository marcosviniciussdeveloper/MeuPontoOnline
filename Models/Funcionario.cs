using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeuPontoOnline.Models
{
    [Table("funcionarios")]
    public class Funcionario
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("nome")]
        public string Nome { get; set; } = string.Empty;

        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Column("senha_hash")]
        public string SenhaHash { get; set; } = string.Empty;

        [Column("tipo_contrato")]
        public string TipoContrato { get; set; } = string.Empty;

        [ForeignKey("Setor")]
        [Column("setor_id")]
        public int SetorId { get; set; }
        public Setor? Setor { get; set; }

        [ForeignKey("Funcao")]
        [Column("funcao_id")]
        public int FuncaoId { get; set; }
        public Funcao? Funcao { get; set; }

        [Column("horario_entrada")]
        public TimeSpan HorarioEntrada { get; set; }

        [Column("horario_saida")]
        public TimeSpan HorarioSaida { get; set; }

        public static implicit operator Funcionario(Funcao v)
        {
            throw new NotImplementedException();
        }
    }
}
