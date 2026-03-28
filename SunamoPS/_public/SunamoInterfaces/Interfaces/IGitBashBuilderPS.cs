namespace SunamoPS._public.SunamoInterfaces.Interfaces;

/// <summary>
/// Interface for building Git bash commands.
/// </summary>
public interface IGitBashBuilderPS
{
    /// <summary>
    /// Gets the list of accumulated commands.
    /// </summary>
    List<string> Commands { get; }

    /// <summary>
    /// Adds a raw git command.
    /// </summary>
    /// <param name="command">Git command to add.</param>
    void Add(string command);

    /// <summary>
    /// Adds a new remote to the repository.
    /// </summary>
    /// <param name="remoteName">Name of the remote to add.</param>
    void AddNewRemote(string remoteName);

    /// <summary>
    /// Appends text to the current command.
    /// </summary>
    /// <param name="text">Text to append.</param>
    void Append(string text);

    /// <summary>
    /// Appends an empty line.
    /// </summary>
    void AppendLine();

    /// <summary>
    /// Appends a line of text.
    /// </summary>
    /// <param name="text">Text to append as a line.</param>
    void AppendLine(string text);

    /// <summary>
    /// Changes the current directory.
    /// </summary>
    /// <param name="path">Target directory path.</param>
    void Cd(string path);

    /// <summary>
    /// Performs a git checkout to the specified branch or commit.
    /// </summary>
    /// <param name="argument">Branch, tag, or commit to checkout.</param>
    void Checkout(string argument);

    /// <summary>
    /// Performs a git clean with the specified options.
    /// </summary>
    /// <param name="options">Git clean options.</param>
    void Clean(string options);

    /// <summary>
    /// Clears all accumulated commands.
    /// </summary>
    void Clear();

    /// <summary>
    /// Clones a git repository.
    /// </summary>
    /// <param name="repoUri">Repository URI to clone.</param>
    /// <param name="arguments">Additional clone arguments.</param>
    void Clone(string repoUri, string arguments);

    /// <summary>
    /// Creates a git commit.
    /// </summary>
    /// <param name="isAddingAllUntrackedFiles">Whether to add all untracked files before committing.</param>
    /// <param name="commitMessage">Commit message text.</param>
    void Commit(bool isAddingAllUntrackedFiles, string commitMessage);

    /// <summary>
    /// Applies a git config command.
    /// </summary>
    /// <param name="configValue">Config setting to apply.</param>
    void Config(string configValue);

    /// <summary>
    /// Fetches from remote repository.
    /// </summary>
    /// <param name="remoteName">Remote name to fetch from.</param>
    void Fetch(string remoteName = "");

    /// <summary>
    /// Initializes a new git repository.
    /// </summary>
    void Init();

    /// <summary>
    /// Merges the specified branch.
    /// </summary>
    /// <param name="branchName">Branch to merge.</param>
    void Merge(string branchName);

    /// <summary>
    /// Pulls from remote repository.
    /// </summary>
    void Pull();

    /// <summary>
    /// Pushes to remote repository.
    /// </summary>
    /// <param name="isForce">Whether to force push.</param>
    void Push(bool isForce);

    /// <summary>
    /// Pushes with custom arguments.
    /// </summary>
    /// <param name="argument">Push arguments.</param>
    void Push(string argument);

    /// <summary>
    /// Manages remote repositories.
    /// </summary>
    /// <param name="argument">Remote command arguments.</param>
    void Remote(string argument);

    /// <summary>
    /// Runs git status.
    /// </summary>
    void Status();

    /// <summary>
    /// Returns all accumulated commands as a string.
    /// </summary>
    /// <returns>String representation of commands.</returns>
    string ToString();
}
