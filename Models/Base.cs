namespace Almanime.Models;

public abstract class Base
{
    public Guid ID { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? ModificationDate { get; set; }
}
