using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace 后台管理系统.Areas.Page.Controllers
{
    public class PageController : Controller
    {
        // GET: Page/Page
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}