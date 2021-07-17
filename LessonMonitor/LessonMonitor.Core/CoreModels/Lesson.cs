using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonMonitor.Core.CoreModels
{
    public class Lesson
    {
        public string YouTubeBroadcastId { get; set; }
        public DateTime StartDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
