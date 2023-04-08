using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.Functions;

namespace WebApplication1.Controllers.Admin
{
    public class ordersController : Controller
    {
        private MobileShoppingEntities db = new MobileShoppingEntities();
    }
}
