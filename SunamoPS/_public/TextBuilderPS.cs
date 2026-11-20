namespace SunamoPS._public;

/// <summary>
/// Represents the text builder PS type.
/// </summary>
public class TextBuilderPS
{
    private bool canUndo = false;
    private int lastIndex = -1;
    private string lastText = "";
    private bool isUsingList = false;

    /// <summary>
    /// Gets or sets the string builder.
    /// </summary>
    public StringBuilder? StringBuilder { get; set; } = null;

    /// <summary>
    /// Gets or sets the prepend every no white.
    /// </summary>
    public string PrependEveryNoWhite { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list.
    /// </summary>
    public List<string>? List { get; set; }

    /// <summary>
    /// Clear.
    /// </summary>
    /// <returns>The clear result.</returns>
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
    /// Create.
    /// </summary>
    /// <returns>The create result.</returns>
    public static TextBuilderPS Create(bool isUsingList = false)
    {
        return new TextBuilderPS(isUsingList);
    }

    /// <summary>
    /// Initializes a new instance of the text builder PS class.
    /// </summary>
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
    /// The can undo.
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
    /// Undo.
    /// </summary>
    /// <param name="what">The what.</param>
    /// <returns>The undo result.</returns>
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
    /// Append.
    /// </summary>
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
    /// Append.
    /// </summary>
    /// <param name="isUsingList">The is using list.</param>
    /// <returns>The append result.</returns>
    public void Append(object value)
    {
        string textString = value.ToString() ?? string.Empty;
        SetUndo(textString);
        Append(textString);
    }

    /// <summary>
    /// Append line.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The append line result.</returns>
    public void AppendLine()
    {
        Append(Environment.NewLine);
    }

    /// <summary>
    /// Append line.
    /// </summary>
    /// <param name="value">The value.</param>
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
    /// To string.
    /// </summary>
    /// <param name="isUsingList">The is using list.</param>
    /// <returns>The to string result.</returns>
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
