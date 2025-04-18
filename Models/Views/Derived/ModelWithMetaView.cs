namespace Almanime.Models.Views.Derived;

public record ModelWithMetaView<T>
{
    public PaginationMetaView Meta { get; init; } = default!;
    public T Models { get; init; } = default!;
}
