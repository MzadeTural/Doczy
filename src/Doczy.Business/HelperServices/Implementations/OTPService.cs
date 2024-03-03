using Doczy.Business.HelperServices.Interfaces;
using Doczy.Core.Entities.Identities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            user.OTP = GenerateOTP();
            user.OTPExpireDate = DateTime.UtcNow.AddMinutes(3);  
        }
    }
}
