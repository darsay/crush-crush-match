using System.Threading.Tasks;

public interface IGameProgressionProvider
{
    Task<bool> Initialize();

    string Load();
    void Save(string text);
}
