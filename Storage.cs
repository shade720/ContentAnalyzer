using System.Collections.Concurrent;
using VkNet.Model;

namespace VkAPITester
{
    public class Storage
    {
        private readonly ConcurrentDictionary<int, DataEntry> _data = new();
        public int Count => _data.Count;

        public void AddEntry(Comment incomeRawDataEntry)
        {
            _data.TryAdd(Count, new DataEntry(
                incomeRawDataEntry.Id, 
                incomeRawDataEntry.PostId ?? 0, 
                incomeRawDataEntry.OwnerId ?? 0,
                incomeRawDataEntry.FromId ?? 0, 
                incomeRawDataEntry.Text, 
                incomeRawDataEntry.Date ?? new DateTime(0,0,0)));
        }

        public void AddRange(List<Comment> list)
        {
            foreach (var comment in list) AddEntry(comment);
        }

        public bool Contains(Comment item)
        {
            return _data.Any(x => x.Value.Id == item.Id);
        }
    }
}
