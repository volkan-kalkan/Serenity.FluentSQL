using System;
using System.Collections;

namespace Serenity
{
    /// <summary>
    ///   This static class contains some helper functions that operate on <see cref="String"/> objects.</summary>
    public static partial class StringHelper
    {
        /// <summary>
        ///   <p>Returns true if <see cref="String"/> is <c>null</c> or empty (zero length)</p></summary>
        /// <remarks>
        ///   <p>This function might be useful if an empty string is assumed to be <c>null</c>.</p>
        ///   <p>This is an extension method, so it can be called directly as <c>str.IsNullOrEmpty()</c>.</p></remarks>
        /// <param name="str">
        ///   String.</param>
        /// <returns>
        ///   If <paramref name="str"/> is <c>null</c> or empty, <c>true</c></returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        ///   <p>Removes whitespace characters in the left or right of the <see cref="String"/> string,
        ///   and if resulting string is empty or null, returns null.</p></summary>
        /// <remarks>
        ///   <p>Generally, when a user entered string is going to be saved to database, if user entered an
        ///   empty string, <c>null</c> or a string of whitespaces, it is stored as <c>null</c>, otherwise
        ///   it is expected to remove whitespace at start and end only.</p>
        ///   <p><b>Warning:</b> "\n" (line end), "\t" (tab) and some other are also considered as whitespace). 
        ///   To see a list see <see cref="String.Trim()" /> function.</p>
        ///   <p>This is an extension method, so it can be called directly as <c>str.TrimToNull()</c>.</p></remarks>
        /// <param name="str">
        ///   String to be trimmed.</param>
        /// <returns>
        ///   Trimmed string, result is null if empty.</returns>
        public static string TrimToNull(this string str)
        {
            if (str.IsNullOrEmpty())
                return null;
            
            str = str.Trim();
            
            if (str.Length == 0)
                return null;
            return str;
        }

        /// <summary>
        ///   <p>Removes whitespace characters in the left or right of the <see cref="String"/> string,
        ///   and if resulting string is empty or null, returns empty.</p></summary>
        /// <remarks>
        ///   <p>Generally, when a user entered string is going to be saved to database, if user entered an
        ///   empty string, <c>null</c> or a string of whitespaces, it is stored as empty string, otherwise
        ///   it is expected to remove whitespace at start and end only.</p>
        ///   <p><b>Warning:</b> "\n" (line end), "\t" (tab) and some other are also considered as whitespace). 
        ///   To see a list see <see cref="String.Trim()" /> function.</p>
        ///   <p>This is an extension method, so it can be called directly as <c>str.TrimToEmpty()</c>.</p></remarks>
        /// <param name="str">
        ///   String to be trimmed.</param>
        /// <returns>
        ///   Trimmed string (result won't be null).</returns>
        public static string TrimToEmpty(this string str)
        {
            if (str.IsNullOrEmpty())
                return String.Empty;
            return str.Trim();
        }

        public static bool IsNullOrEmpty(this ICollection collection)
        {
            return collection == null || collection.Count == 0;
        }
    }
}