using Doczy.Business.HelperServices.Interfaces;
using Doczy.Core.Entities.Identities;
using Doczy.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Doczy.Business.HelperServices.Implementations
{
    public class OTPCleanupService : IOTPCleanupService
    {
        private readonly IBaseAppUserRepository _userRepository;
        private readonly UserManager<BaseAppUser> _userManager;

        public OTPCleanupService(IBaseAppUserRepository userRepository, UserManager<BaseAppUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task CleanupExpiredOTPAsync()
        {
            var usersWithExpiredOTP = await _userRepository.GetUsersWithExpiredOTPAsync();

            foreach (var user in usersWithExpiredOTP)
            {
                user.OTP = null;
                user.OTPExpiryDate = null;
                await _userManager.UpdateAsync(user);
            }
        }
    }
}
