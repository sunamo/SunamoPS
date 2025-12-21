namespace SunamoPS.Data;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
public class PowershellMethod
{
    public PowershellMethod(string name, string content, int line)
    {
        Name = name;
        Content = content;
        Line = line;
    }

    public string Name { get; set; }
    public string Content { get; set; }
    public int Line { get; set; }
}