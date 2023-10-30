using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;

namespace netUtilities
{
    /// <summary>
    /// Třída pro získání verze .NET Frameworku pro danou assembly.
    /// </summary>
    public sealed class VerzeDotNetFrameworkuProAssembly
    {
        /// <summary>
        /// Získá verzi .NET Frameworku pro danou assembly.
        /// </summary>
        /// <param name="souborSAssembly">Cesta k souboru s assembly.</param>
        /// <returns>Verze .NET Frameworku.</returns>
        public static string ZiskejVerziDotNetFrameworku(string souborSAssembly)
        {
            try
            {
                Assembly assembly = Assembly.LoadFrom(souborSAssembly);
                var reference = assembly.GetCustomAttributes<TargetFrameworkAttribute>().FirstOrDefault();
                return reference?.FrameworkName ?? "Nepodařilo se získat verzi .NET Frameworku";
            }
            catch (Exception ex)
            {
                return $"Chyba při získávání verze .NET Frameworku: {ex.Message}";
            }
        }
    }
}
