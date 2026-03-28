namespace SunamoPS._public.SunamoInterfaces.Interfaces;

/// <summary>
/// Interface for building NPM bash commands.
/// </summary>
public interface INpmBashBuilderPS
{
    /// <summary>
    /// Runs npm install with optional arguments.
    /// </summary>
    /// <param name="arguments">Optional npm install arguments.</param>
    void Install(string? arguments = null);
}
