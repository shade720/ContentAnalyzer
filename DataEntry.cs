using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkAPITester
{
    public class DataEntry
    {
        public long Id { get; set; }
        public long AuthorId { get; set; }
        public string Text { get; set; }
        public DateTime PostDate { get; set; }

    }
}
