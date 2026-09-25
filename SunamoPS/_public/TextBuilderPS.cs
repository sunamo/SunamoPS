namespace SunamoPS._public;

/// <summary>
/// Text builder that supports both StringBuilder and List-based modes for constructing PowerShell commands.
/// </summary>
public class TextBuilderPS
{
    private bool canUndo = false;
    private int lastIndex = -1;
    private string lastText = "";
    private bool isUsingList = false;

    /// <summary>
    /// Gets or sets the underlying StringBuilder instance. Null when using list mode.
    /// </summary>
    public StringBuilder? StringBuilder { get; set; } = null;

    /// <summary>
    /// Gets or sets the text to prepend before every non-whitespace append.
    /// </summary>
    public string PrependEveryNoWhite { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of command strings. Used in list mode.
    /// </summary>
    public List<string>? List { get; set; }

    /// <summary>
    /// Clears all accumulated content.
    /// </summary>
    public void Clear()
    {
        if (isUsingList)
        {
            List?.Clear();
        }
        else
        {
            StringBuilder?.Clear();
        }
    }

    /// <summary>
    /// Creates a new TextBuilderPS instance.
    /// </summary>
    /// <param name="isUsingList">Whether to use list mode instead of StringBuilder mode.</param>
    /// <returns>New TextBuilderPS instance.</returns>
    public static TextBuilderPS Create(bool isUsingList = false)
    {
        return new TextBuilderPS(isUsingList);
    }

    /// <summary>
    /// Initializes a new instance of TextBuilderPS.
    /// </summary>
    /// <param name="isUsingList">Whether to use list mode instead of StringBuilder mode.</param>
    public TextBuilderPS(bool isUsingList = false)
    {
        this.isUsingList = isUsingList;
        if (isUsingList)
        {
            List = new List<string>();
        }
        else
        {
            StringBuilder = new StringBuilder();
        }
    }

    /// <summary>
    /// Gets or sets whether undo is enabled. When disabled, resets undo state.
    /// </summary>
    public bool CanUndo
    {
        get
        {
            if (isUsingList)
            {
                return false;
            }
            return canUndo;
        }
        set
        {
            canUndo = value;
            if (!value)
            {
                lastIndex = -1;
                lastText = "";
            }
        }
    }

    private void UndoIsNotAllowed(string what)
    {
        ThrowEx.IsNotAllowed(what);
    }

    /// <summary>
    /// Undoes the last append operation. Only supported in StringBuilder mode.
    /// </summary>
    public void Undo()
    {
        if (isUsingList)
        {
            UndoIsNotAllowed("Undo");
        }
        if (lastIndex != -1 && StringBuilder != null)
        {
            StringBuilder.Remove(lastIndex, lastText.Length);
        }
    }

    /// <summary>
    /// Appends text without a trailing newline.
    /// </summary>
    /// <param name="text">Text to append.</param>
    public void Append(string text)
    {
        if (isUsingList)
        {
            if (List != null && List.Count > 0)
            {
                List[List.Count - 1] += text;
            }
            else
            {
                List?.Add(text);
            }
        }
        else
        {
            SetUndo(text);
            StringBuilder?.Append(PrependEveryNoWhite);
            StringBuilder?.Append(text);
        }
    }

    private void SetUndo(string text)
    {
        if (isUsingList)
        {
            UndoIsNotAllowed("SetUndo");
        }
        if (CanUndo && StringBuilder != null)
        {
            lastIndex = StringBuilder.Length;
            lastText = text;
        }
    }

    /// <summary>
    /// Appends an object's string representation.
    /// </summary>
    /// <param name="value">Object to append.</param>
    public void Append(object value)
    {
        string textString = value.ToString() ?? string.Empty;
        SetUndo(textString);
        Append(textString);
    }

    /// <summary>
    /// Appends a newline.
    /// </summary>
    public void AppendLine()
    {
        Append(Environment.NewLine);
    }

    /// <summary>
    /// Appends text followed by a newline.
    /// </summary>
    /// <param name="text">Text to append as a line.</param>
    public void AppendLine(string text)
    {
        if (isUsingList)
        {
            List?.Add(PrependEveryNoWhite + text);
        }
        else
        {
            SetUndo(text);
            StringBuilder?.Append(PrependEveryNoWhite + text + Environment.NewLine);
        }
    }

    /// <summary>
    /// Returns the accumulated text as a string.
    /// </summary>
    /// <returns>String representation of accumulated content.</returns>
    public override string ToString()
    {
        if (isUsingList)
        {
            return string.Join(Environment.NewLine, List ?? new List<string>());
        }
        else
        {
            return StringBuilder?.ToString() ?? string.Empty;
        }
    }
}
