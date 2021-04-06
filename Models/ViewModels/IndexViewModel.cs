using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbbyWakeAss10.Models.ViewModels
{
    public class IndexViewModel
    {
       public List<Bowler> Bowler { get; set; }
        public PageNumberInfo PageNumberInfo { get; set; }

        public string TeamCategory { get; set; }
    }
}
