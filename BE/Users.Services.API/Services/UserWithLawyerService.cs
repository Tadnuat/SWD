using Users.Services.API.Models.Dtos;

using Users.Services.API.Services.IService;
// USer - Lawyer (REF: UserID)
public class UserWithLawyerProfileService : IUserWithLawyerProfileService
{
    private readonly IUserService _userService;
    private readonly LawyerProfileApiClient _lawyerApiClient;

    public UserWithLawyerProfileService(IUserService userService, LawyerProfileApiClient lawyerApiClient)
    {
        _userService = userService;
        _lawyerApiClient = lawyerApiClient;
    }

    public async Task<IEnumerable<UserWithLawyerProfileDTO>> GetAllUsersWithLawyerProfileAsync()
    {
        var usersResponse = await _userService.GetAllUsersAsync();
        var result = new List<UserWithLawyerProfileDTO>();

        foreach (var user in usersResponse.Result)
        {
            LawyerProfileDTO? lawyerProfile = null;
            if (user.Role == "Lawyer")
            {
                lawyerProfile = await _lawyerApiClient.GetByUserIdAsync(user.Id);
            }
            result.Add(new UserWithLawyerProfileDTO
            {
                User = user,
                LawyerProfile = lawyerProfile
            });
        }
        return result;
    }

    public async Task<UserWithLawyerProfileDTO?> GetUsersWithLawyerProfileByIdAsync(int userId)
    {
        var userResponse = await _userService.GetUserByIdAsync(userId);
        var user = userResponse.Result;
        if (user == null)
            return null;

        LawyerProfileDTO? lawyerProfile = null;
        if (user.Role == "Lawyer")
        {
            lawyerProfile = await _lawyerApiClient.GetByUserIdAsync(user.Id);
        }

        return new UserWithLawyerProfileDTO
        {
            User = user,
            LawyerProfile = lawyerProfile
        };
    }

    public async Task<IEnumerable<UserWithLawyerProfileDTO>> GetUsersWithLawyerProfileOnlyAsync()
    {
        var usersResponse = await _userService.GetAllUsersAsync();
        var result = new List<UserWithLawyerProfileDTO>();

        foreach (var user in usersResponse.Result)
        {
            if (user.Role == "Lawyer")
            {
                var lawyerProfile = await _lawyerApiClient.GetByUserIdAsync(user.Id);
                if (lawyerProfile != null)
                {
                    result.Add(new UserWithLawyerProfileDTO
                    {
                        User = user,
                        LawyerProfile = lawyerProfile
                    });
                }
            }
        }
        return result;
    }
}