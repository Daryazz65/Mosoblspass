using System.Collections.Generic;

namespace Mosoblspass.AppData
{
    public static class SearchHistory
    {
        private static List<string> _history = new List<string>();
        public static IReadOnlyList<string> History => _history.AsReadOnly();

        public static void Add(string query)
        {
            if (!string.IsNullOrWhiteSpace(query))
                _history.Add(query);
        }
    }
}
