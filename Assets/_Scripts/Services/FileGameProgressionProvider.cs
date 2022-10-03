using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class FileGameProgressionProvider : IGameProgressionProvider
{
    private static string _path = Application.persistentDataPath + "/savefile.json";


    public async Task<bool> Initialize()
    {
        await Task.Yield();
        return true;
    }

    public string Load()
    {
        if (File.Exists(_path))
        {
            return File.ReadAllText(_path);
        }

        return string.Empty;
    }

    public void Save(string data)
    {
        System.IO.File.WriteAllText(Application.persistentDataPath + "/savefile.json", data);
    }
}
