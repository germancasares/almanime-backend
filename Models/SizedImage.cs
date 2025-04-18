namespace Almanime.Models;

public class SizedImage
{
    public SizedImage(Uri? tiny, Uri? small, Uri? original)
    {
        Tiny = tiny;
        Small = small;
        Original = original;
    }

    public Uri? Tiny { get; set; }
    public Uri? Small { get; set; }
    public Uri? Original { get; set; }
}
