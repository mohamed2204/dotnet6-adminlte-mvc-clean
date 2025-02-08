using System.Collections.Generic;

namespace AdminLTE.MVC.ViewModel
{
    public class ControllerActionViewModel
    {
        public string ControllerName { get; set; }
        public List<string> ActionsNames { get; set; }
    }

    public class ApplicationControllersViewModel
    {
        public List<ControllerActionViewModel> Controllers { get; set; }
    }



}