using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication1.Models;
using WebApplication1.Models.Functions;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        MobileShoppingEntities db = new MobileShoppingEntities();
        [AllowAnonymous]
        public ActionResult SignIn()
        {
            if (Session["email"] != null) return RedirectToAction("index", "Home");
            return View(new SignInVM());
        }
        [AllowAnonymous]
        public ActionResult SignUp()
        {
            if (Session["email"] != null) return RedirectToAction("SignUp", "User");
            return View(new SignUpVM());
        }
        public ActionResult Account()
        {
            if (Session["email"] == null)
            {
                Session["TrangTruoc"] = Request.RawUrl;
                return Redirect("SignIn");
            }
            var TaiKhoan = (user)Session["email"];
            var TaiKhoanAccount = new user
            {
                email = TaiKhoan.email,
                passwords = TaiKhoan.passwords,
            };
            return View(TaiKhoanAccount);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(SignInVM tk)
        {
            if (ModelState.IsValid)
            {
                var list = db.users.Where(m => m.email == tk.email && m.passwords == tk.password).ToList();
                if (list.Count == 0)
                {
                    ModelState.AddModelError("email", "Tài Khoản hoặc Mật Khẩu không chính xác");
                    return View(tk);
                }
                user taiKhoan = list.First();
                if (taiKhoan.is_admin != false)
                {
                    FormsAuthentication.SetAuthCookie(taiKhoan.email, tk.AutoLogin);
                    return RedirectToAction("Index", "Admin");
                }
                Session["email"] = taiKhoan;
                if (tk.AutoLogin)
                {
                    HttpCookie ckTaiKhoan = new HttpCookie("email"), ckpasswords = new HttpCookie("passwords");
                    ckTaiKhoan.Value = taiKhoan.email; ckpasswords.Value = taiKhoan.passwords;
                    ckTaiKhoan.Expires = DateTime.Now.AddDays(15);
                    ckpasswords.Expires = DateTime.Now.AddDays(15);
                    Response.Cookies.Add(ckTaiKhoan);
                    Response.Cookies.Add(ckpasswords);
                }
                if (Session["TrangTruoc"] != null)
                {
                    return Redirect(Session["TrangTruoc"].ToString());
                }
                return RedirectToAction("index", "Home");
            }
            return View(tk);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(SignUpVM tk)
        {
            if (ModelState.IsValid)
            {
                var taiKhoan = db.users.Where(x => x.email == tk.Email).FirstOrDefault();
                if (taiKhoan != null)
                {
                    ModelState.AddModelError("Email", "Tài Khoản đã tồn tại");
                    return View(tk);
                }
                taiKhoan = new user()
                {   
                    totalspend = 0,
                    id_user = tk.id_user,
                    email = tk.Email,
                    passwords = tk.Password,
                    is_admin = false,
                    //user_type = "user",
                };
                var hTK = new Func_User();
                hTK.Insert(taiKhoan);
                return RedirectToAction("SignIn", "User");
            }
            return View(tk);
        }
        public ActionResult LogOut()
        {
            Session["email"] = null;
            HttpCookie ckTaiKhoan = new HttpCookie("email"), ckpasswords = new HttpCookie("passwords");
            ckTaiKhoan.Expires = DateTime.Now.AddDays(-1);
            ckpasswords.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(ckTaiKhoan);
            Response.Cookies.Add(ckpasswords);
            return RedirectToAction("SignIn", "User");
        }
        public ActionResult Edit_Info(int id)
        {
            var user = db.users.Find(id);
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit_Info(user model, HttpPostedFileBase avatar)
        {
            if (ModelState.IsValid)
            {
                Session["email"] = model;
                var users = db.users.Where(x => x.id_user == model.id_user).FirstOrDefault();
                if (avatar != null && avatar.ContentLength > 0)
                {
                    int id = model.id_user;

                    string File_name = "";
                    int index = avatar.FileName.IndexOf('.');
                    File_name = "user" + id.ToString() + "." + avatar.FileName.Substring(index + 1);
                    string path = Path.Combine(Server.MapPath("~/wwwroot/Images/Avatars"), File_name);
                    avatar.SaveAs(path);

                    model.avatar = File_name;
                }
                else if (avatar == null)
                {
                    model.avatar = users.avatar;
                }
                var fuser = new Func_User();
                fuser.Update(model);
                return RedirectToAction("Details_Info", "User", new { id = model.id_user });
            }
            return View(model);
        }
        public ActionResult Details_Info()
        {
            user user = Session["email"] as user;
            user users = db.users.Find(user.id_user);
            return View(users);
        }
        public ActionResult Orders()
        {
            user user = Session["email"] as user;
            user users = db.users.Find(user.id_user);
            var orders = db.orders.Where(x => x.id_user == user.id_user).ToList();
            return View(orders);
        }
        public ActionResult History()
        {
            user user = Session["email"] as user;
            user users = db.users.Find(user.id_user);
            var orders = db.orders.Where(x => x.id_user == user.id_user).ToList();
            return View(orders);

        }
    }
}