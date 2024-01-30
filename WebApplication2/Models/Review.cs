using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.Models
{
    [Table("Reviews")]
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReviewID { get; set; }

        [ForeignKey("Book")]
        public int BookID { get; set; }
        public string? ReviewText { get; set; }

        public virtual Book? Book { get; set; }
    }
}