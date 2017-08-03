using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheInspector
{
    public class CacheConversion
    {
        private readonly Dictionary<string, Func<object, object>> _cacheConversions;

        public CacheConversion()
        {
            _cacheConversions = new Dictionary<string, Func<object, object>>();
        }

        public void Add(string cacheKey, Func<object, object> conversion)
        {
            if (!_cacheConversions.ContainsKey(cacheKey))
            {
                _cacheConversions.Add(cacheKey, conversion);
            }
        }

        public string GetValue(string cacheKey, object cacheValue, bool showDetails)
        {
            foreach (var cacheConversion in _cacheConversions)
            {
                if (cacheKey.StartsWith(cacheConversion.Key, StringComparison.InvariantCultureIgnoreCase))
                {
                    var typedObject = cacheConversion.Value(cacheValue);

                    if (typedObject == null)
                    {
                        return "N\\A";
                    }

                    if (typedObject is IEnumerable)
                    {
                        return showDetails ? ConvertToString((IEnumerable)typedObject) : GetCount((IEnumerable)typedObject);
                    }
                    return ConvertToString(typedObject);
                }
            }

            if (cacheValue is IEnumerable)
            {
                return showDetails ? ConvertToString((IEnumerable)cacheValue) : GetCount((IEnumerable)cacheValue);
            }

            return showDetails ? ConvertToString(cacheValue) : cacheValue.ToString();
        }

        private string GetCount(IEnumerable list)
        {
            return $"{list} ({list.Cast<object>().Count()})";
        }

        private string ConvertToString(IEnumerable list)
        {
            var sb = new StringBuilder();

            foreach (var listItem in list)
            {
                sb.AppendLine();

                foreach (var info in listItem.GetType().GetProperties())
                {
                    try
                    {
                        var propValue = info.GetValue(listItem, null) ?? "(null)";
                        sb.AppendLine($"\t{info.Name}: {propValue}");
                    }
                    catch 
                    {
                        sb.AppendLine($"\t{info.Name}: N/A");
                    }
                   
                }
            }

            return sb.ToString();
        }

        private string ConvertToString(object obj)
        {
            var sb = new StringBuilder();

            sb.AppendLine();

            foreach (var info in obj.GetType().GetProperties())
            {
                try
                {
                    var propValue = info.GetValue(obj, null) ?? "(null)";
                    sb.AppendLine($"\t{info.Name}: {propValue}");
                }
                catch 
                {
                    sb.AppendLine($"\t{info.Name}: N/A");
                }
            }
           
            return sb.ToString();
        }
    }
}
