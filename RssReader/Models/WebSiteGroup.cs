using System.ComponentModel.DataAnnotations;

namespace RssReader.Models
{
    public class WebSiteGroup
    {
        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        ///     サイトの表示名
        /// </summary>
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}