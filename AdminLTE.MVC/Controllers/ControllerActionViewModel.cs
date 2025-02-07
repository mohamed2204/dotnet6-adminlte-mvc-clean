using System.Collections.Generic;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;

namespace AdminLTE.MVC.Controllers
{
    public class ControllerActionViewModel
    {
       public string  ControllerName { get; set; }
        public List<string> ActionsNames { get; set; } 
    }    

    public class ApplicationControllersViewModel
    {
        public List<ControllerActionViewModel> Controllers { get; set; }
    }
    


}