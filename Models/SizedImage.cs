namespace Almanime.Models;

public class SizedImage(Uri? tiny, Uri? small, Uri? original)
{

    public Uri? Tiny { get; set; } = tiny;
    public Uri? Small { get; set; } = small;
    public Uri? Original { get; set; } = original;
}
