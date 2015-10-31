using leishi.Common;
using leishi.DataModel.Models;
using leishi.Web.Core;
using leishi.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Practices.Unity;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace leishi.Web.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// The model factory.
        /// </summary>
        private readonly ModelFactory modelFactory;

        public AccountController([Dependency] ModelFactory modelFactory)
        {
            this.modelFactory = modelFactory;
        }
        
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // GET: /Account/Register
        public ActionResult Register()
        {
            return View(this.modelFactory.CreateModel(typeof(RegisterViewModel)) as RegisterViewModel);
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = model.UserName, WangWang = model.UserName };
                var result = await HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await HttpContext.GetOwinContext().GetUserManager<ApplicationSignInManager>().SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}