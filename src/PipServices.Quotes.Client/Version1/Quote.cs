using System.Runtime.Serialization;

namespace PipServices.Quotes.Client.Version1
{
    [DataContract]
    public class Quote
    {
        [DataMember(Name = "id", IsRequired = true)]
        public string Id { get; set; }

        [DataMember(Name = "text", IsRequired = true)]
        public MultiString Text { get; set; }

        [DataMember(Name = "author", IsRequired = true)]
        public MultiString Author { get; set; }

        // Writing, Translating, Verifying, Completed
        [DataMember(Name = "status", IsRequired = true)]
        public string Status { get; set; }

        [DataMember(Name = "tags", IsRequired = false)]
        public string[] Tags { get; set; }

        [DataMember(Name = "all_tags", IsRequired = false)]
        public string[] AllTags { get; set; }
    }
}
