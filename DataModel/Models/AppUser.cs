using leishi.Common.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace leishi.DataModel.Models
{
    public class AppUser : IdentityUser<string, AppLogin, AppUserRole, AppClaim>
    {
        [Column("UserType")]
        public UserTypes UserType { get; set; }

        public string WangWang { get; set; }

        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

        public AppUser()
            : base()
        {
            Id = Guid.NewGuid().ToString();
            DateCreated = DateTime.Now;
        }
    }
}
