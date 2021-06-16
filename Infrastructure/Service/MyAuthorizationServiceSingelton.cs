using Core.Enum;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public class MyAuthorizationServiceSingelton : IMyAuthorizationServiceSingelton
    {
        IHttpContextAccessor accessor;

        #region Members

        private long userId;
        private UserRole role;

        public MyAuthorizationServiceSingelton(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }

        #endregion Members

        public long UserIdAuthenticated
        {
            get
            {
                this.AssertAuthenticated();
                return this.userId;
            }
        }

        #region ---Private---
        protected void AssertAuthenticated()
        {
            this.ExtractClaims(true);
            if (this.userId <= 0 || (int)this.role < 0)
            {
                throw new Exception("Unauthorized");
            }
        }

        private void ExtractClaims(bool throwError)
        {
            ClaimsIdentity claimsIdentity = this.accessor.HttpContext.User.Identity as ClaimsIdentity;

            if (claimsIdentity is null || claimsIdentity.Claims is null)
            {
                if (throwError)
                {
                    throw new Exception("model = null");
                }
                else
                {
                    return;
                }
            }

            if (claimsIdentity.Claims.Any())
            {
                Claim claimUserId = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "userId");

                if (claimUserId != null)
                {
                    this.userId = Convert.ToInt64(claimUserId.Value);
                }

                Claim claimRoleId = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultRoleClaimType);

                if (claimRoleId != null)
                {
                    this.role = Enum.Parse<UserRole>(claimRoleId.Value);
                }
            }
        }

        #endregion
    }
}
