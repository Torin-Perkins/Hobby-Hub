using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hobby_Hub
{

    public class NavigationlPageMasterMenuItem
    {
        public NavigationlPageMasterMenuItem()
        {
            TargetType = typeof(NavigationlPageMasterMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}