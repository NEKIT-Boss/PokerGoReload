using System;
using System.Composition;
using System.Diagnostics;
using Prism.Logging;

namespace PokerGo
{
    [Export(typeof(ILoggerFacade))]
    public class DebugLogger : ILoggerFacade
    {
        public void Log(string message, Category category, Priority priority)
        {
            Debug.WriteLine($"LOG [{DateTime.Now:T}] {message} in {category} category with {priority} priority.");
        }
    }
}