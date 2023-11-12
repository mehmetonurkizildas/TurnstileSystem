using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.ComponentModel;

namespace System
{
    public static class StringHelper
    {
        public static T? ToJson<T>(this string str)
        {
            try
            {
                if (string.IsNullOrEmpty(str))
                {
                    return default;
                }
                return JsonConvert.DeserializeObject<T>(str)!;
            }
            catch (Exception)
            {
                return default;
            }
        }
        public static string ToJson(this object obj, bool isLowerCase = false)
        {
            try
            {
                if (isLowerCase)
                {
                    var serializerSettings = new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        NullValueHandling = NullValueHandling.Ignore,
                        DefaultValueHandling = DefaultValueHandling.Ignore

                    };
                    return JsonConvert.SerializeObject(obj, serializerSettings);
                }
                return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                });
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public static DateTime UnixTimeStampToDateTime(this long? unixTimestamp)
        {
            if (unixTimestamp == null) return DateTime.MinValue;

            DateTime unixEpoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return unixEpoch.AddSeconds(unixTimestamp ?? 0);
        }

        public static string GetEnumDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = Attribute.GetCustomAttribute(field!, typeof(DescriptionAttribute)) as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }
        public static DateTime StartOfDay(this DateTime theDate)
        {
            return theDate.Date;
        }

        public static DateTime EndOfDay(this DateTime theDate)
        {
            return theDate.Date.AddDays(1).AddTicks(-1);
        }
    }
}
