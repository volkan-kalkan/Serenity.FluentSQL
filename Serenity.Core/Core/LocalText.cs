
namespace Serenity
{
    public class LocalText
    {
        private string text;

        public LocalText(string text)
        {
            this.text = text;
        }

        public override string ToString()
        {
            return text;
        }
    }
}