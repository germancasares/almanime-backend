namespace Almanime.Models.Views.Derived;

public readonly record ModelWithMetaView<T>
{
    public PaginationMetaView Meta { get; init; } = default!;
    public T Models { get; init; } = default!;
}
