using System;
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
        //public ActionResult LichSu()
        //{
        //    if (Session["email"] == null) return Redirect("SignIn");
        //    user taiKhoan = (user)Session["email"];
        //    TimeSpan day = new TimeSpan(0, 0, 0, 0);
        //    DateTime dateHomNay = DateTime.Now;
        //    var listLichSu = db.datphongs.Where(dp => dp.ID_ND == taiKhoan.ID_ND).Join(db.phongs, dp => dp.ID_P, p => p.ID_P, (dp, p) => new
        //    {
        //        ID_DP = dp.ID_DP,
        //        tenphong = p.tenphong,
        //        ngayden = dp.ngayden,
        //        ngaydi = dp.ngaydi,
        //        tongtien = dp.tongtien,
        //        ngaydat = dp.ngaydat

        //    }).AsEnumerable().Select(m =>
        //        new lichsuView()
        //        {
        //            ID_DP = m.ID_DP,
        //            tenphong = m.tenphong,
        //            ngaydat = m.ngaydat.Value.ToString("dd/MM/yyyy"),
        //            ngayden = m.ngayden.Value.ToString("dd/MM/yyyy"),
        //            ngaydi = m.ngaydi.Value.ToString("dd/MM/yyyy"),
        //            tongtien = m.tongtien,
        //            cothehuy = m.ngayden <= dateHomNay ? false : true

        //        }).ToList();
        //    return View(listLichSu);
        //}
        public ActionResult SignUpSuccess()
        {
            return View(new user());
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
        //public ActionResult XoaDatPhong()
        //{
        //    int MaDatPhong = Convert.ToInt32(RouteData.Values["id"].ToString());
        //    DateTime today = DateTime.Now;
        //    var ktHuyPhong = db.datphongs.Where(x => x.ID_DP == MaDatPhong && x.ngayden <= today).ToList();
        //    if (ktHuyPhong.Count == 0)
        //    {
        //        var HamDP = new Func_datphong();
        //        HamDP.Delete(MaDatPhong);
        //        TempData["HuyDat"] = 1;
        //        return RedirectToAction("LichSu", "Account");
        //    }
        //    return RedirectToAction("LichSu", "Account");
        //}
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
                    id_user = tk.id_user,
                    email = tk.Email,
                    passwords = tk.Password,
                    is_admin = false,
                };
                var hTK = new Func_User();
                hTK.Insert(taiKhoan);
                return RedirectToAction("SignIn", "User");
            }
            return View(tk);
        }
        public ActionResult SuaAccount()
        {
            int ID_ND = Convert.ToInt32(RouteData.Values["id"].ToString());
            var user = db.users.Find(ID_ND);
            return View(user);
        }
        [HttpPost]
        public ActionResult SuaAccount(user tk)
        {
            if (ModelState.IsValid)
            {
                //var taiKhoan = new user()
                //{
                //    email = tk.email,
                //    passwords = tk.passwords,
                //    hoten = tk.hoten,
                //    sdt = tk.sdt,
                //    email = tk.email,
                //    diachi = tk.diachi,
                //    role_type = 1
                //};
                Session["email"] = tk;
                var HamTK = new Func_User();
                HamTK.Update(tk);
                return RedirectToAction("Account", "Account");
            }
            return View(tk);
        }
    }
}