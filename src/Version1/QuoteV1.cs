namespace PipServices.Quotes.Client.Version1
{
    public class QuoteV1
    {
        public string Id { get; set; }
        public MultiString Text { get; set; }
        public MultiString Author { get; set; }
        public string Status { get; set; }
        public string[] Tags { get; set; }
        public string[] All_Tags { get; set; }

        public QuoteV1()
        {
        }

        public override bool Equals(object obj)
        {
            var quote = obj as QuoteV1;

            return quote != null &&
                quote.Id.Equals(Id) &&
                quote.Text.Equals(Text) &&
                quote.Author.Equals(Author) &&
                quote.Status.Equals(Status);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
