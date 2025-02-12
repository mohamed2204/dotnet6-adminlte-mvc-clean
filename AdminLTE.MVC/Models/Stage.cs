using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdminLTE.MVC.Models
{
    public partial class Stage
    {
        public Stage()
        {
            StagePhases = new HashSet<StagePhase>();
        }

        public long Id { get; set; }
        //[Required, StringLength(256)]
        [Required(ErrorMessage = "Un nom est requis"), StringLength(256)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Un nom de promotion est requis"), StringLength(256)]
        public string Promotion { get; set; }

        public virtual ICollection<StagePhase> StagePhases { get; set; }
    }
}
