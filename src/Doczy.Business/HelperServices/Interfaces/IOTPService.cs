using Doczy.Core.Entities.Identities;

namespace Doczy.Business.HelperServices.Interfaces
{
    public interface IOTPService
    {
        public string GenerateOTP();
        void SetOTPForUser(BaseAppUser user);
    }
}
