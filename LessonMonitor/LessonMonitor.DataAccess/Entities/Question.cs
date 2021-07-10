using System;
using System.Collections.Generic;

#nullable disable

namespace LessonMonitor.DataAccess.Entities
{
    public partial class Question : BaseEntity
    {
        public int UserId { get; set; }
        public string Description { get; set; }
        public virtual User User { get; set; }
    }
}
