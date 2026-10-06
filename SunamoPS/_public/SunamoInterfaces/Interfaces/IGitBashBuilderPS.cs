namespace SunamoPS._public.SunamoInterfaces.Interfaces;

public interface IGitBashBuilderPS
{
    List<string> Commands { get; }

    void Add(string command);

    void AddNewRemote(string remoteName);

    void Append(string text);

    void AppendLine();

    void AppendLine(string text);

    void Cd(string path);

    void Checkout(string argument);

    void Clean(string options);

    void Clear();

    void Clone(string repoUri, string arguments);

    void Commit(bool isAddingAllUntrackedFiles, string commitMessage);

    void Config(string configValue);

    void Fetch(string remoteName = "");

    void Init();

    void Merge(string branchName);

    void Pull();

    void Push(bool isForce);

    void Push(string argument);

    void Remote(string argument);

    void Status();

    string ToString();
}
