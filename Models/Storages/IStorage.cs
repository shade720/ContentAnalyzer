namespace VkAPITester.Models.Storages
{
    public interface IStorage
    {
        public void Add(DataEntry comment);
        public void AddRange(IEnumerable<DataEntry> comments);
        public void Clear();
    }
}
