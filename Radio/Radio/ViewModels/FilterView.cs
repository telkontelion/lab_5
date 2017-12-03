using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Radio.ViewModelss
{

    public class FilterView
    {
        public FilterView(string NameS, int? GroupId, string Name, string Description)
        {
            SelectedGroupId = GroupId;
            SelectedName = Name;
            SelectedDescription = Description;
            SelectedNameS = NameS;
        }


        public int? SelectedGroupId { get; set; }
        public string SelectedName { get; set; }
        public string SelectedDescription { get; set; }
        public string SelectedNameS { get; set; }
    }

}
