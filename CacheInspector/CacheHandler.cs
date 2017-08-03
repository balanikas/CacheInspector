using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheInspector
{
    class CacheHandler
    {
        internal static string GetCache(Command command)
        {
            var enumerator = System.Web.HttpRuntime.Cache.GetEnumerator();

            var cacheDictionary = new Dictionary<string, string>();
            var sb = new StringBuilder();
            while (enumerator.MoveNext())
            {
                var key = (string)enumerator.Key;
                if (!key.StartsWith(command.Key, StringComparison.InvariantCultureIgnoreCase))
                {
                    continue;
                }

                var converted = Inspector.Instance.CacheConversion.GetValue(key, enumerator.Value, command.ShowDetails);

                cacheDictionary.Add(key, converted);
            }

            foreach (var entry in cacheDictionary.OrderBy(x => x.Key))
            {
                sb.AppendLine($"{entry.Key,-50}{entry.Value,10}");
            }
            sb.AppendLine($"count: {cacheDictionary.Count}");

            return sb.ToString();
        }

        public static string ClearCache(Command command)
        {
            var counter = 0;
            foreach (DictionaryEntry entry in System.Web.HttpRuntime.Cache)
            {
                var key = (string) entry.Key;
                if (key.StartsWith(command.Key, StringComparison.InvariantCultureIgnoreCase))
                {
                    System.Web.HttpRuntime.Cache.Remove(key);
                    counter++;
                }
            }
            return $"cleared: {counter}";
        }
    }
}
