using System.ComponentModel.DataAnnotations;

namespace AdminLTE.MVC.ViewModel
{
    public class RoleFormViewModel
    {
        [Required, StringLength(256)]
        public string Name { get; set; }
    }
}