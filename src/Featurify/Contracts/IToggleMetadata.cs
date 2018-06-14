namespace Featurify.Contracts
{
    public interface IToggleMetadata
    {
        string Name { get; set; }
        bool Value { get; set; }
        string UserId { get; set; }
    }
}
