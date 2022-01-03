﻿using System;
using System.Text;
using UnityEngine;

namespace Backtrace.Unity.Model
{
    /// <summary>
    /// Unity message representation in Backtrace-Unity library
    /// </summary>
    internal class BacktraceUnityMessage
    {
        private readonly string _formattedMessage;
        public readonly string Message;
        public readonly string StackTrace;
        public readonly LogType Type;

        public BacktraceUnityMessage(BacktraceReport report)
        {
            if (report == null)
            {
                throw new ArgumentException("report");
            }
            Message = report.Message;

            if (report.ExceptionTypeReport)
            {
                Type = LogType.Exception;
                StackTrace = GetFormattedStackTrace(report.Exception.StackTrace);
                _formattedMessage = GetFormattedMessage(true);
            }
            else
            {
                StackTrace = string.Empty;
                Type = LogType.Warning;
                _formattedMessage = GetFormattedMessage(true);
            }
        }
        public BacktraceUnityMessage(string message, string stacktrace, LogType type)
        {
            Message = message;
            StackTrace = GetFormattedStackTrace(stacktrace);
            Type = type;
            _formattedMessage = GetFormattedMessage(false);
        }

        private string GetFormattedMessage(bool backtraceFrame)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat(
                       "[{0}] {1}<{2}>: {3}", new object[4] {
                            DateTime.Now.ToUniversalTime().ToString(),
                            backtraceFrame ? "(Backtrace)" : string.Empty,
                            Enum.GetName(typeof(LogType), Type),
                            Message}
                       );

            // include stack trace if log was generated by exception/error
            if (IsUnhandledException())
            {
                stringBuilder.AppendLine();
                stringBuilder.Append(string.IsNullOrEmpty(StackTrace) ? "No stack trace available" : StackTrace);
            }
            return stringBuilder.ToString();
        }

        private string GetFormattedStackTrace(string stacktrace)
        {
            return !string.IsNullOrEmpty(stacktrace) && stacktrace.EndsWith("\n")
                ? stacktrace.Remove(stacktrace.LastIndexOf("\n"))
                : stacktrace;
        }

        /// <summary>
        /// Return information if current Unity message contain information about error or exception
        /// </summary>
        /// <returns>True if unity message is an exception/error message</returns>
        public bool IsUnhandledException()
        {
            return ((Type == LogType.Exception || Type == LogType.Error)
                && !string.IsNullOrEmpty(Message));
        }

        /// <summary>
        /// Convert Backtrace Untiy Message to string that will be acceptable by source code format
        /// </summary>
        /// <returns>Source code string</returns>
        public override string ToString()
        {
            return _formattedMessage;
        }
    }
}
