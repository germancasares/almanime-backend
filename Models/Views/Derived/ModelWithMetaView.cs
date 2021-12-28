namespace Almanime.Models.Views.Derived;

public record ModelWithMetaView<T>
{
    public PaginationMetaView Meta { get; set; } = default!;
    public T Models { get; set; } = default!;
}
