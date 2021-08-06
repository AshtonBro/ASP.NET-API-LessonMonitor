using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using static LessonMonitor.API.CustomErrors.ErrorMessageRegistry;

namespace LessonMonitor.API.Attributes
{
    /// <summary>
    /// Custom Required Attribute has the ability to display an error in several languages ​​RU and EN.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class CustomRequiredAttribute : RequiredAttribute
    {
        public Elang Language { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomRequiredAttribute"/> class.
        /// </summary>
        /// <param name="type">Model type.</param>
        /// <param name="language">Error language.</param>
        /// <param name="propertyName">Name property which need to validate.</param>
        public CustomRequiredAttribute([NotNull] Type type, Elang language, [CallerMemberName] string propertyName = null)
        {
            Language = language;

            if (language == Elang.Ru)
            {
                ErrorMessage = GetRuError(type, propertyName);
            }
            else if (language == Elang.En)
            {
                ErrorMessage = GetEnError(type, propertyName);
            }
        }
    }
}
