using Doczy.Business.HelperServices.Interfaces;
using Doczy.Core.Entities.Identities;

namespace Doczy.Business.HelperServices.Implementations
{
    public class OTPService : IOTPService
    {
        public string GenerateOTP()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        public void SetOTPForUser(BaseAppUser user)
        {
            throw new NotImplementedException();
        }
    }
}
