using System;

namespace LessonMonitor.API.Contracts
{
    /// <summary>
    /// Lesson model contains prop: Id, YouTubeBroadcastId, StartDate, Title, Description.
    /// </summary>
    public class Lesson
    {
        /// <summary>
        /// Lesson id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id YouTube broadcast, we can assign after the end of the lesson.
        /// </summary>
        public string YouTubeBroadcastId { get; set; }

        /// <summary>
        /// Broadcast start date.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Lesson topic.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Description contains homework's context.
        /// </summary>
        public string Description { get; set; }
    }
}