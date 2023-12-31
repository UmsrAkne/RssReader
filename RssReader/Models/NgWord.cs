using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RssReader.Models
{
    public class NgWord
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Word { get; set; } = string.Empty;

        [NotMapped]
        public int Index { get; set; }

        public bool IsMatch(string target)
        {
            return !string.IsNullOrWhiteSpace(Word) && target.Contains(Word);
        }
    }
}