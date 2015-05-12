using System;
using System.Collections.Generic;

namespace HgCo.WindowsLive.SkyDrive.Support
{
    /// <summary>
    /// Provides extensions methods for various data types.
    /// </summary>
	internal static class Extensions
	{
        /// <summary>
        /// Iterates through a collection and executes the action for each item.
        /// </summary>
        /// <typeparam name="T">The type of items in the collection.</typeparam>
        /// <param name="collection">The collection to iterate.</param>
        /// <param name="action">The action to execute.</param>
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            if (collection != null)
            {
                foreach (var item in collection)
                {
                    action(item);
                }
            }
        }
    }
}
