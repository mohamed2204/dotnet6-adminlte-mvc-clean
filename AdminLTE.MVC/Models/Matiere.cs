using System;
using System.Collections.Generic;

namespace AdminLTE.MVC.Models
{
    public partial class Matiere
    {
        public int Id { get; set; }

        public string Name { get; set; }

       
    }

    public partial class Stage
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Promotion { get; set; }


    }

    public partial class Phase
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }

    //public partial class StagePhases
    //{
    //    public int StageId { get; set; }

    //    public int PhaseId { get; set; }

    //}
}
