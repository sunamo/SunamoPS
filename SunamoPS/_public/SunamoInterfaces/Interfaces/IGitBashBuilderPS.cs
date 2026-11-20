namespace SunamoPS._public.SunamoInterfaces.Interfaces;

/// <summary>
/// Represents the i git bash builder PS type.
/// </summary>
public interface IGitBashBuilderPS
{
    /// <summary>
    /// Gets the commands.
    /// </summary>
    List<string> Commands { get; }

    /// <summary>
    /// Add.
    /// </summary>
    /// <param name="command">The command.</param>
    /// <returns>The add result.</returns>
    void Add(string command);

    /// <summary>
    /// Add new remote.
    /// </summary>
    /// <param name="command">The command.</param>
    void AddNewRemote(string remoteName);

    /// <summary>
    /// Append.
    /// </summary>
    /// <param name="remoteName">The remote name.</param>
    void Append(string text);

    /// <summary>
    /// Append line.
    /// </summary>
    /// <param name="text">The text.</param>
    void AppendLine();

    /// <summary>
    /// Append line.
    /// </summary>
    void AppendLine(string text);

    /// <summary>
    /// Cd.
    /// </summary>
    /// <param name="text">The text.</param>
    void Cd(string path);

    /// <summary>
    /// Checkout.
    /// </summary>
    /// <param name="path">The path.</param>
    void Checkout(string argument);

    /// <summary>
    /// Clean.
    /// </summary>
    /// <param name="argument">The argument.</param>
    void Clean(string options);

    /// <summary>
    /// Clear.
    /// </summary>
    /// <param name="options">The options.</param>
    void Clear();

    /// <summary>
    /// Clone.
    /// </summary>
    void Clone(string repoUri, string arguments);

    /// <summary>
    /// Commit.
    /// </summary>
    /// <param name="repoUri">The repo uri.</param>
    /// <param name="arguments">The arguments.</param>
    void Commit(bool isAddingAllUntrackedFiles, string commitMessage);

    /// <summary>
    /// Config.
    /// </summary>
    /// <param name="isAddingAllUntrackedFiles">The is adding all untracked files.</param>
    /// <param name="commitMessage">The commit message.</param>
    void Config(string configValue);

    /// <summary>
    /// Fetch.
    /// </summary>
    /// <param name="configValue">The config value.</param>
    void Fetch(string remoteName = "");

    /// <summary>
    /// Init.
    /// </summary>
    /// <param name="remoteName">The remote name.</param>
    void Init();

    /// <summary>
    /// Merge.
    /// </summary>
    void Merge(string branchName);

    /// <summary>
    /// Pull.
    /// </summary>
    /// <param name="branchName">The branch name.</param>
    void Pull();

    /// <summary>
    /// Push.
    /// </summary>
    void Push(bool isForce);

    /// <summary>
    /// Push.
    /// </summary>
    /// <param name="isForce">The is force.</param>
    void Push(string argument);

    /// <summary>
    /// Remote.
    /// </summary>
    /// <param name="argument">The argument.</param>
    void Remote(string argument);

    /// <summary>
    /// Status.
    /// </summary>
    /// <param name="argument">The argument.</param>
    void Status();

    /// <summary>
    /// To string.
    /// </summary>
    string ToString();
}
