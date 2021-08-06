using System.ComponentModel.DataAnnotations;

namespace LessonMonitor.API.Contracts
{
    public class NewMember
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string YouTubeUserId { get; set; }
    }
}
