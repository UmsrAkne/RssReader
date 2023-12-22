using System.ComponentModel.DataAnnotations;

namespace RssReader.Models
{
    public class NgWord
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Word { get; set; } = string.Empty;

        public bool IsMatch(string target)
        {
            return !string.IsNullOrWhiteSpace(Word) && target.Contains(Word);
        }
    }
}