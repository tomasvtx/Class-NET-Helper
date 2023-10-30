using System;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;
using System.Threading.Tasks;
using Logger.Tasks;
using static Logger.Model.Enums;
using Dialogs;

namespace netUtilities
{
    /// <summary>
    /// Třída pro reportování vnitřních chyb softwaru.
    /// </summary>
    public static class ReportInternalErrorsAsync
    {
        /// <summary>
        /// Asynchronní metoda pro reportování vnitřních chyb softwaru.
        /// </summary>
        /// <param name="metoda">Metoda, ve které došlo k chybě.</param>
        /// <param name="dispatcher">Dispatcher pro práci s UI vláknem.</param>
        /// <param name="chyba">Výjimka, která byla zachycena.</param>
        /// <returns>Asynchronní úkol.</returns>
        public static async Task ReportInternalErrorAsync(this MethodBase metoda, Dispatcher dispatcher, Exception chyba, MyApplication.IMyApp iMyApp)
        {
            string celneJmenoMetody = string.Empty;
            string Err = string.Empty;

            try { 
            celneJmenoMetody = $"{metoda?.DeclaringType?.FullName}.{metoda?.Name}";
            Err = $"{celneJmenoMetody}\n{chyba?.Message}";
            }
            catch (Exception ex) { }


            try
            {
                await await Logger.Tasks.LoggerPublic.LoggerTitleInternal(iMyApp, Err, metoda, GetState.Get(ProcessState.SwError));
            }
            catch { }

            try
            {
                if (dispatcher == null)
                {
                    return;
                }
                await dispatcher?.InvokeAsync(() =>
                {
                    CustomMessageBox customMessageBox = new CustomMessageBox("Interní chyba softwaru", Err, MessageBoxButton.OK, "OK, zavři dialog");
                
                    customMessageBox?.ShowDialog();
                }, DispatcherPriority.Normal);
            }
            catch { }
        }
    }
}
