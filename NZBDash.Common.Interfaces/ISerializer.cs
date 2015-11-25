namespace NZBDash.Common.Interfaces
{
    public interface ISerializer
    {
        T SerializeXmlData<T>(string uri) where T : new();
        T SerializedJsonData<T>(string url) where T : new();
    }
}
