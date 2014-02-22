using System;

namespace Serenity.Data
{
    /// <summary>
    ///   Extensions for objects implementing IDbWhere interface.</summary>
    public static class FilterableQueryExtensions
    {
        /// <summary>
        ///   Adds a filter to query</summary>
        /// <typeparam name="T">
        ///   Query class</typeparam>
        /// <param name="self">
        ///   Query</param>
        /// <param name="filter">
        ///   Filter</param>
        /// <returns>
        ///   Query itself.</returns>
        public static T Where<T>(this T self, BaseCriteria filter) where T: IFilterableQuery
        {
            if (!ReferenceEquals(null, filter) && !filter.IsEmpty)
            {
                var statement = filter.ToString(self);
                self.Where(statement);
            }
            return self;
        }
    }
}