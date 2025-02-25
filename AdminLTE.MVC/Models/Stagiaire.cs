using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdminLTE.MVC.Models
{
    public partial class Stagiaire
    {
        public Stagiaire()
        {
            StagiaireStages = new HashSet<StagiaireStage>();
        }

        public long Id { get; set; }
       
        //[Required(ErrorMessage = "Le grade est requis"), StringLength(10)]
        //public string Grade { get; set; }
       
        [Required(ErrorMessage = "Le prénom est requis"), StringLength(50)]
        [Display(Name = "Prénom")]
        public string Prenom { get; set; }
        
        [Required(ErrorMessage = "Le nom est requis"), StringLength(50)]
        public string Nom { get; set; }
       
        [Required(ErrorMessage = "Le Mle est requis"), StringLength(12)]
        [Display(Name = "Matricule")]
        public string Mle { get; set; }
      
        [Required(ErrorMessage = "Le numéro de la CNI est requis"), StringLength(10)]
        [Display(Name = "N° de la CNI")]
        public string Cin { get; set; }

        //[Required(ErrorMessage = "Un nom en arabe est requis"), StringLength(256)]
        [Display(Name = "Nom en arabe")]
        public string NomAr { get; set; }

        //[Required(ErrorMessage = "Un nom est requis"), StringLength(256)]
        [Display(Name = "Prénom et arabe")]
        public string PrenomAr { get; set; }
       
        [Required(ErrorMessage = "La spécialité est requise")]
        [Display(Name = "Spacilaité")]
        public long? SpecialiteId { get; set; }
      
        public string Branche { get; set; }

        [Required(ErrorMessage = "La propotion est requise"), StringLength(256)]
        public string Promotion { get; set; }

        [Required(ErrorMessage = "Le Grade est requis")]
        [Display(Name = "Grade")]
        public long? GradeId { get; set; }

        public virtual Grade Grade { get; set; }

        public virtual Specialite Specialite { get; set; }
        public virtual ICollection<StagiaireStage> StagiaireStages { get; set; }
    }
}
