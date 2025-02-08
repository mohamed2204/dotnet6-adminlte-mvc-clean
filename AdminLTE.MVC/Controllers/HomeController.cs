using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AdminLTE.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;
using AdminLTE.MVC.ViewModel;

namespace AdminLTE.MVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        //[AllowAnonymous]
        public IActionResult Index()
        {

            var model = new ApplicationControllersViewModel
            {
                Controllers = GetControllersAndActions()
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public List<ControllerActionViewModel> GetControllersAndActions()
        {
            var controllersActionList = new List<ControllerActionViewModel>();
            var controllers = Assembly.GetExecutingAssembly().GetTypes()
              .Where(type => typeof(Controller).IsAssignableFrom(type) && !type.IsAbstract);


            foreach (var controller in controllers)
            {
                var actions = controller.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public)
                    .Where(m => IsActionMethod(m))
                    .Select(x => x.Name)
                    .Distinct()
                    .ToList();

                controllersActionList.Add(new ControllerActionViewModel
                {
                    ControllerName = controller.Name.Replace("Controller", ""),
                    ActionsNames = actions
                });
                //Debug.Write(actions);
            }

            return controllersActionList;
        }

        private bool IsActionMethod(MethodInfo methodInfo)
        {
            bool IsActionResult = typeof(IActionResult).IsAssignableFrom(methodInfo.ReturnType);
            bool IsTaskIActionResult = methodInfo.ReturnType.IsGenericType &&
                                        methodInfo.ReturnType.GetGenericTypeDefinition() == typeof(Task<>) &&
                                        typeof(IActionResult).IsAssignableFrom(methodInfo.ReturnType.GetGenericArguments()[0]);


            return IsActionResult || IsTaskIActionResult;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
