using System;
using System.Collections.Generic;

namespace AdminLTE.MVC.Models
{
    public partial class Phase
    {
        public Phase()
        {
            StagePhases = new HashSet<StagePhase>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<StagePhase> StagePhases { get; set; }
    }
}
