
using Newtonsoft.Json;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System.ComponentModel.DataAnnotations.Schema;
using ColumnAttribute = Supabase.Postgrest.Attributes.ColumnAttribute;
using TableAttribute = Supabase.Postgrest.Attributes.TableAttribute;

namespace MeuPontoOnline.Models
{
    [Table("funcionarios")]
    public class Funcionario : BaseModel
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("nome")]
        public string Nome { get; set; } = string.Empty;

        [Column("data_nascimento")] 
        public DateTime DataNascimento {get; set; }



        [Column("email")]
        public string Email { get; set; } = string.Empty;


        [Column("senha_hash")]
        public string SenhaHash { get; set; } = string.Empty;

        
        [Column("matricula")]
        public string Matricula { get; set; } = string.Empty;


        [Column("tipo_contrato")]
        public string TipoContrato { get; set; } = string.Empty;

        [Column("setor_id")]
        public int SetorId { get; set; }

        [JsonIgnore]
        public Setor? Setor { get; set; }

        [JsonIgnore]
        public Funcao? Funcao { get; set; }

        [Column("funcao_id")]
        public int FuncaoId { get; set; }

   
        [Column("horario_entrada")]
        public TimeSpan HorarioEntrada { get; set; }

        [Column("horario_saida")]
        public TimeSpan HorarioSaida { get; set; }
    }
}
