using System;

namespace LessonMonitor.Core.Exceptions
{
    public class MemberException : Exception
    {
        public MemberException(string message) : base(message)
        {
        }

        public MemberException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
