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
        public string Description { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        public bool IsRead { get; set; }
    }
}