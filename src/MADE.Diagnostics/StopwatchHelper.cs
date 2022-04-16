// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Defines a helper class for creating <see cref="Stopwatch"/> instances for method calls to track how long they take to execute.
    /// </summary>
    public static class StopwatchHelper
    {
        private static readonly Dictionary<string, Stopwatch> Stopwatches = new();

        /// <summary>
        /// Starts a <see cref="Stopwatch"/> with the specified <paramref name="caller"/> and <paramref name="name"/>.
        /// </summary>
        /// <param name="caller">
        /// The caller for the <see cref="Stopwatch"/>, assumed as the file path by the caller if not set.
        /// </param>
        /// <param name="name">
        /// The name of the unit of code under test, assumed as the member name of the caller if not set.
        /// </param>
        /// <returns>
        /// A display message for an output containing the <see cref="Stopwatch"/> key.
        /// </returns>
        public static string Start([CallerFilePath] string caller = null, [CallerMemberName] string name = null)
        {
            string key = $"{caller}_{name}";

            if (Stopwatches.ContainsKey(key))
            {
                return null;
            }

            KeyValuePair<string, Stopwatch> stopwatch = Stopwatches.FirstOrDefault(
                kvp => kvp.Key.Equals(key, StringComparison.CurrentCultureIgnoreCase));

            if (stopwatch.Value != null)
            {
                return null;
            }

            var stopWatch = new Stopwatch();
            Stopwatches.Add(key, stopWatch);
            stopWatch.Start();

            return $"Stopwatch '{key}' started.";
        }

        /// <summary>
        /// Stops a <see cref="Stopwatch"/> with the specified <paramref name="caller"/> and <paramref name="name"/>.
        /// </summary>
        /// <param name="caller">
        /// The caller for the <see cref="Stopwatch"/>, assumed as the file path by the caller if not set.
        /// </param>
        /// <param name="name">
        /// The name of the unit of code under test, assumed as the member name of the caller if not set.
        /// </param>
        /// <returns>
        /// A display message for an output containing the <see cref="Stopwatch"/> details of the elapsed time, and the elapsed time value.
        /// </returns>
        public static (string, TimeSpan) Stop([CallerFilePath] string caller = null, [CallerMemberName] string name = null)
        {
            string key = $"{caller}_{name}";

            if (!Stopwatches.ContainsKey(key))
            {
                return (null, TimeSpan.Zero);
            }

            KeyValuePair<string, Stopwatch> stopwatch = Stopwatches.FirstOrDefault(
                kvp => kvp.Key.Equals(key, StringComparison.CurrentCultureIgnoreCase));

            if (stopwatch.Value == null)
            {
                return (null, TimeSpan.Zero);
            }

            stopwatch.Value.Stop();
            Stopwatches.Remove(key);

            TimeSpan elapsed = stopwatch.Value.Elapsed;
            return ($"Stopwatch '{key}' took {elapsed} to run.", elapsed);
        }
    }
}