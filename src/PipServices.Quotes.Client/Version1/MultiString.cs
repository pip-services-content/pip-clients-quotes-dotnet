using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PipServices.Quotes.Client.Version1
{
    // This is a temporary implementation
    // We need to think how to make extensible multi-string class
    // that can be serialized by standard JSON data serializer
    [DataContract]
    public class MultiString
    {
        [DataMember(Name = "en", IsRequired = false)]
        public string En { get; set; }

        [DataMember(Name = "ru", IsRequired = false)]
        public string Ru { get; set; }

        [DataMember(Name = "de", IsRequired = false)]
        public string De { get; set; }

        [DataMember(Name = "fr", IsRequired = false)]
        public string Fr { get; set; }

        [DataMember(Name = "sp", IsRequired = false)]
        public string Sp { get; set; }

        [DataMember(Name = "pt", IsRequired = false)]
        public string Pt { get; set; }

        public Dictionary<string, object> ToDictionary()
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            if (En != null) result.Add("en", En);
            if (Ru != null) result.Add("ru", Ru);
            if (De != null) result.Add("de", De);
            if (Fr != null) result.Add("fr", Fr);
            if (Sp != null) result.Add("sp", Sp);
            if (Pt != null) result.Add("pt", Pt);

            return result;
        }
    }
}
