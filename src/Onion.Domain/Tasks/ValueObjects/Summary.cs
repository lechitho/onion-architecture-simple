namespace Onion.Domain.Tasks.ValueObjects
{
    public readonly struct Summary
    {
        private readonly string _text;

        public Summary(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentNullException($"Summary value is required");
            }

            _text = text;
        }

        public override string ToString()
        {
            return _text;
        }
    }
}
