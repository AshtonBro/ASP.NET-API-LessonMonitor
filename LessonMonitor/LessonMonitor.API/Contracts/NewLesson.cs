using System;
using System.ComponentModel.DataAnnotations;

namespace LessonMonitor.API.Contracts
{
    /// <summary>
    /// NewLesson is created new lesson.
    /// </summary>
    public class NewLesson
    {
        /// <summary>
        /// Id YouTube broadcast, we can assign after the end of the lesson.
        /// </summary>
        [Required]
        public string YouTubeBroadcastId { get; set; }

        /// <summary>
        /// Broadcast start date.
        /// </summary>
        [Required]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Lesson topic.
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Description contains homework's context.
        /// </summary>
        [Required]
        public string Description { get; set; }
    }
}
