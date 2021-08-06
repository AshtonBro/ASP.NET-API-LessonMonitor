using System;

namespace LessonMonitor.API.Contracts
{
    /// <summary>
    /// Homework model.
    /// </summary>
    public class Homework
    {
        /// <summary>
        /// Homework id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Homework topic.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Description contains homework's context.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Presumably a GitHub link.
        /// </summary>
        public Uri Link { get; set; }
    }
}
