using MarkDown.Enums;
using MarkDown.Tags;
using MarkDown.Interfaces;
using MarkDown.TagContexts;

namespace MarkDown.Tags;

public class HeaderTag : Tag
{
    public override TagName TagName => TagName.Header;

    public override string HtmlOpen => "<h1>";
    public override string HtmlClose => "</h1>";
    public override string MarkDownOpen => "# ";
    public override string MarkDownClose => System.Environment.NewLine;

    public HeaderTag(MarkDownEnvironment environment) : base(environment)
    {
    }

    public override TagContext CreateContext(int startIndex, TagContext nowContext)
    {
        return new HeaderContext(startIndex, nowContext, this);
    }

    public override bool CanCreateContext(string text, int position)
    {
        if (position > 0 && text[position - 1].ToString() != System.Environment.NewLine)
            return false;
        
        return text.Substring(position, MarkDownOpen.Length).Equals(MarkDownOpen);
    }

    public override bool IsClosePosition(string text, int position)
    {
        return position == text.Length - 1
               || text[position + 1].ToString().Equals(System.Environment.NewLine);
    }
}