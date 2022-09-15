using System.Threading.Tasks;
using Unity.Services.Authentication;

public class AuthGameService : IService
{
    public async Task Initialize()
    {
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }

    public void Clear()
    {
    }
}