using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNet.Controllers
{
    /*Контроллер Home представляет папку Home которая расположена внутри папки Views. Контроллер определяет нужное представление которое запросил пользователь*/
    /*Контроллер Home связан с каждым .cshtml файлом с помощью методов чьи названия соответсвуют названиям .cshtml файлам*/
    /*return View() - возвращает представление с таким же именем как у метода из которого он был вызван*/
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}