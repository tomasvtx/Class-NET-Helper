using System;
using System.Runtime.InteropServices;

namespace netUtilities
{
    /// <summary>
    /// Třída pro úpravu vzhledu okenního rámu ve WPF aplikaci.
    /// </summary>
    public sealed class WpfWindowChrome
    {
        /// <summary>
        /// Zakáže tlačítko pro zavření okna v okenním rámu.
        /// </summary>
        /// <param name="hwnd">Handle okna.</param>
        public static void ZakazatTlacitkoZavreni(IntPtr hwnd)
        {
            IntPtr hMenu = ZiskatSystemoveMenu(hwnd, false);
            PovolitPolozkuMenu(hMenu, ScZavrit, MfZakazano);
        }

        /// <summary>
        /// Zakáže všechna tlačítka v okenním rámci.
        /// </summary>
        /// <param name="hwnd">Handle okna.</param>
        public static void ZakazatVsechnaTlacitka(IntPtr hwnd)
        {
            NastavitStylOkennihoRamu(hwnd, ZiskatStylOkennihoRamu(hwnd) & ~WsSysmenu);
        }

        /// <summary>
        /// Třída pro práci se systémovým menu.
        /// </summary>
        [DllImport("user32.dll")]
        static extern IntPtr ZiskatSystemoveMenu(IntPtr hWnd, bool bRevert);

        /// <summary>
        /// Třída pro práci s manipulací nad položkami systémového menu.
        /// </summary>
        [DllImport("user32.dll")]
        static extern bool PovolitPolozkuMenu(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

        /// <summary>
        /// Třída pro práci s okenním rámem a jeho stylovými vlastnostmi.
        /// </summary>
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int ZiskatStylOkennihoRamu(IntPtr hWnd);


        /// <summary>
        /// Třída pro práci s nastavením stylu okenního rámu.
        /// </summary>
        [DllImport("user32.dll")]
        private static extern int NastavitStylOkennihoRamu(IntPtr hWnd, int dwNewLong);

        const uint MfZakazano = 0x00000001; 
        const uint MfPovoleno = 0x00000000; 
        const uint ScZavrit = 0xF060;

        const int WsSysmenu = 0x80000;
    }

}
