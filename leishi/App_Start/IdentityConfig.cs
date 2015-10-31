using leishi.DataModel;
using leishi.DataModel.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace leishi.Web
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
    
    public class AppUserStore : UserStore<AppUser, AppRole, string, AppLogin, AppUserRole, AppClaim>
    {
        public AppUserStore(ApplicationDBContext context)
            : base(context)
        {
        }
    }

    public class AppPasswordHasher : PasswordHasher
    {
        public ApplicationDBContext DbContext { get; set; }

        // Custom hashing used before migrating to Identity
        public static string GetMD5Hash(string value)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        // Verify if the password is hashed using MD5. If yes, rehash using ASP.NET Identity Crypto which is more secure
        // this is invoked when old users try to login. Eventually all the old the password are rehashed to a more secure hash
        public override PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            if (String.Equals(hashedPassword, GetMD5Hash(providedPassword), StringComparison.InvariantCultureIgnoreCase))
            {
                ReHashPassword(hashedPassword, providedPassword);
                return PasswordVerificationResult.Success;
            }

            return base.VerifyHashedPassword(hashedPassword, providedPassword);
        }

        // Rehash password using ASP.NET Identity Crypto
        // Store it back into database
        private void ReHashPassword(string hashedPassword, string providedPassword)
        {
            var user = DbContext.Users.Where(x => x.PasswordHash == hashedPassword).FirstOrDefault();

            user.PasswordHash = base.HashPassword(providedPassword);

            // Update SecurityStamp with new Guid to nullify any previous cookies
            user.SecurityStamp = Guid.NewGuid().ToString();

            DbContext.SaveChanges();
        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<AppUser, string>
    {
        public ApplicationUserManager(IUserStore<AppUser, string> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new AppUserStore(context.Get<ApplicationDBContext>()));

            manager.UserValidator = new UserValidator<AppUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Use the custom password hasher to validate existing user credentials
            manager.PasswordHasher = new AppPasswordHasher() { DbContext = context.Get<ApplicationDBContext>() };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<AppUser>
            {
                MessageFormat = "您的验证码是：{0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<AppUser>
            {
                Subject = "雷士照明-验证码",
                BodyFormat = "您的验证码是：{0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<AppUser>(dataProtectionProvider.Create("【雷士照明售后系统】"));
            }
            return manager;
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<AppUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(AppUser user)
        {
            return UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
