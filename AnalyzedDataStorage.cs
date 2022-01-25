using System.Collections.Concurrent;
using VkNet.Model;

namespace VkAPITester
{
    public class AnalyzedDataStorage
    {
        private readonly ConcurrentBag<DataEntry> _data = new();
        public int Count => _data.Count;

        public void AddEntry(Comment incomeRawDataEntry)
        {
            _data.Add(new DataEntry
            {
                Id = incomeRawDataEntry.Id, 
                AuthorId = incomeRawDataEntry.OwnerId ?? 0,
                PostDate = incomeRawDataEntry.Date ?? new DateTime(0,0,0),
                Text = incomeRawDataEntry.Text
            });
        }

        public void AddRange(List<Comment> list)
        {
            foreach (var comment in list) AddEntry(comment);
        }

        public bool Contains(Comment item)
        {
            return _data.Any(x => x.Id == item.Id);
        }
    }
}
