using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mosoblspass.AppData
{
    public static class SearchHistory
    {
        private static List<string> _history = new List<string>();
        public static IReadOnlyList<string> History => _history.AsReadOnly();
        public static void Add(string query)
        {
            if (!string.IsNullOrWhiteSpace(query))
                _history.Insert(0, query);
            if (_history.Count > 50)
                _history.RemoveAt(_history.Count - 1);
        }
    }
}