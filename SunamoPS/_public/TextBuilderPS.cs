namespace SunamoPS._public;

public class TextBuilderPS
{
    private bool canUndo = false;
    private int lastIndex = -1;
    private string lastText = "";
    private bool isUsingList = false;

    public StringBuilder? StringBuilder { get; set; } = null;

    public string PrependEveryNoWhite { get; set; } = string.Empty;

    public List<string>? List { get; set; }

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

    public static TextBuilderPS Create(bool isUsingList = false)
    {
        return new TextBuilderPS(isUsingList);
    }

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

    public void Append(object value)
    {
        string textString = value.ToString() ?? string.Empty;
        SetUndo(textString);
        Append(textString);
    }

    public void AppendLine()
    {
        Append(Environment.NewLine);
    }

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
