namespace SunamoPS._public.SunamoInterfaces.Interfaces;

/// <summary>
/// Represents the i npm bash builder PS type.
/// </summary>
public interface INpmBashBuilderPS
{
    /// <summary>
    /// Install.
    /// </summary>
    /// <param name="arguments">The arguments.</param>
    void Install(string? arguments = null);
}
