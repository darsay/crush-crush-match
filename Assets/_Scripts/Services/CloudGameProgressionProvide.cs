using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.CloudSave;
using UnityEngine;

public class CloudGameProgressionProvider : IGameProgressionProvider
{
    private static string _cloudData = string.Empty;

    public async Task<bool> Initialize()
    {
        Dictionary<string, string> savedData = await CloudSaveService.Instance.Data.LoadAsync();

        savedData.TryGetValue("data", out _cloudData);
        

        return true;
    }

    public string Load()
    {
        return _cloudData;
    }

    public void Save(string text)
    {
        CloudSaveService.Instance.Data.ForceSaveAsync(new Dictionary<string, object> { { "data", text } })
            .ContinueWith(t => Debug.LogException(t.Exception), TaskContinuationOptions.OnlyOnFaulted);
    }

    
}
