using System.Threading.Tasks;
using Unity.Services.Core;
using Unity.Services.Core.Environments;

public class ServicesInitializer
{
    private string _environmentId;

    public ServicesInitializer(string environmentId)
    {
        _environmentId = environmentId;
    }

    public async Task Initialize()
    {
        InitializationOptions options = new InitializationOptions();
        if (!string.IsNullOrEmpty(_environmentId))
        {
            options.SetEnvironmentName(_environmentId);
        }

        await UnityServices.InitializeAsync(options);
    }
}
