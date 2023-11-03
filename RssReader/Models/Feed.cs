using System;
using System.ComponentModel.DataAnnotations;

namespace RssReader.Models
{
    public class Feed
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int ParentSiteId { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Url { get; set; } = string.Empty;

        [Required]
        public bool IsRead { get; set; }
    }
}