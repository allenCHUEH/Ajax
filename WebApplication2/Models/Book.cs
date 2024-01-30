using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.Models
{
    [Table("Book")]
    public class Book
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//識別規格
        public int BookId { get; set; }


        [StringLength(64)]
        public string? Title { get; set; }

        [StringLength(32)]
        public string? Author { get; set; }


        [MaxLength(13)]
        [Column(TypeName ="nchar")]
        public string? ISBN { get; set; }


        public virtual ICollection<Review>? Reviews { get; set; }
    }
}