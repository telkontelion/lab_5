using Radio.ViewModelss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Radio.ViewModels
{
    public class IndexViewModels
    {
        public IEnumerable<Artists> Artists { get; set; }
        public IEnumerable<Ganrs> Ganrs { get; set; }
        public IEnumerable<Groups> Groups { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterView FilterView { get; set; }
    }
}
