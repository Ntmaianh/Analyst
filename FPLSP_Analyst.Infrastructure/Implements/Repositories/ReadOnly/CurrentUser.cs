using FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FPLSP_Analyst.Infrastructure.Implements.Repositories.ReadOnly
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public List<string> RoleCodes => GetRoleClaim(ClaimTypes.Role);
        public string Picture => GetClaimValue<string>(FPLSP_Token.Consts.ClaimTypes.Picture);
        string ICurrentUser.Name => GetClaimValue<string>(ClaimTypes.Name);
        string ICurrentUser.Email => GetClaimValue<string>(ClaimTypes.Email);
        string ICurrentUser.UserName => GetClaimValue<string>(FPLSP_Token.Consts.ClaimTypes.UserName);
        List<string> ICurrentUser.RoleNames => GetRoleClaim(FPLSP_Token.Consts.ClaimTypes.RoleNames);

        public Guid? Id
        {
            get
            {
                var nameIdentifier = GetClaimValue<string>(FPLSP_Token.Consts.ClaimTypes.Id);
                if (string.IsNullOrEmpty(nameIdentifier) || !Guid.TryParse(nameIdentifier, out _)) return new();
                return Guid.Parse(nameIdentifier);
            }
        }

        public Guid? IdTrainingFacility
        {
            get
            {
                var idTrainingFacility = GetClaimValue<string>(FPLSP_Token.Consts.ClaimTypes.IdTrainingFacility);
                if (string.IsNullOrEmpty(idTrainingFacility) || !Guid.TryParse(idTrainingFacility, out _)) return new();
                return new Guid(idTrainingFacility);
            }
        }

        private List<string> GetRoleClaim(string claimType)
        {
            try
            {
                var claim = _httpContextAccessor.HttpContext.User.FindAll(claimType);
                if (claim == null) return default;
                var claims = new List<string>();
                foreach (var claimValue in claim) claims.Add(claimValue.Value);
                return claims;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private T GetClaimValue<T>(string claimType)
        {
            try
            {
                var claim = _httpContextAccessor.HttpContext.User.FindFirst(claimType);
                if (claim == null) return default;

                return (T)Convert.ChangeType(claim.Value, typeof(T));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
