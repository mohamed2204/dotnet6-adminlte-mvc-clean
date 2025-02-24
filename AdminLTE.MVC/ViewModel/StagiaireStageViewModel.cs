using System.ComponentModel.DataAnnotations;
using AdminLTE.MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace AdminLTE.MVC.ViewModel
{
    [Keyless]
    public class StagiaireStageViewModel
    {
        //public long Id { get; set; }
        [Required(ErrorMessage = "Un Stagiaire est requis")]
        public long StagiaireId { get; set; }

        [Required(ErrorMessage = "Un Stage est requis")]
        public long StageId { get; set; }

        [Required(ErrorMessage = "Une Specialité est requise")]
        public long SpecialiteId { get; set; }

        [Required(ErrorMessage = "La date de Début est requise")]
        public string DateDebut { get; set; }
        
        [Required(ErrorMessage = "La date de fin est requise")]
        public string DateFin { get; set; }

        public virtual Specialite Specialite { get; set; }
        public virtual Stagiaire Stagiaire { get; set; }

        public virtual Stage Stage { get; set; }

    }
}
