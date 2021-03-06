using System;
using UnityEngine;

namespace Backtrace.Unity.Types
{
    /// <summary>
    /// Report type that Backtrace client will generate
    /// </summary>
    [Flags]
    public enum ReportFilterType
    {
        /// <summary>
        /// Don't use report filter feature
        /// </summary>
        [Tooltip("Disable report filtering.")]
#if UNITY_2019_2_OR_NEWER
        [InspectorName("Disable")]
#endif
        None = 0,
        /// <summary>
        /// Ignore deduplication strategy
        /// </summary>
        [Tooltip("String message report.")]
        Message = 1,

        /// <summary>
        /// Handled exception
        /// </summary>
        [Tooltip("Handled exception.")]
#if UNITY_2019_2_OR_NEWER
        [InspectorName("Handled exception")]
#endif
        Exception = 2,
        /// <summary>
        /// Unhandled exception - Error and Exception messages generated by Unity Logger.
        /// </summary>
        [Tooltip("Game unhandled exception.")]
#if UNITY_2019_2_OR_NEWER
        [InspectorName("Unhandled exception")]
#endif
        UnhandledException = 4,

        /// <summary>
        /// Game hang
        /// </summary>
        [Tooltip("Game hang.")]
        Hang = 8
    }
}
