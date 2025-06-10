using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LetterBoxdDomain
{
    public class User
    {
        public int Id { get; set; }

        [Column(TypeName = "character varying(20)")]
        [MaxLength(20)]
        public required string UserName { get; set; }

        [Column(TypeName = "character varying(128)")]
        [MaxLength(128)]
        public required string PasswordHash { get; set; }
    }
}
