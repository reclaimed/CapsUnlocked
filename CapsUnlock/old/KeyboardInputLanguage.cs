using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapsUnlock
{
    // thanks to Abdelmawgoud https://code.msdn.microsoft.com/windowsdesktop/Changing-Keyboard-Layout-d50b27da
    class KeyboardInputLanguage
    {

        private List<InputLanguage> langs = new List<InputLanguage>();

        private int currentLang;

        public KeyboardInputLanguage()
        {

            // get a list of all installed languages in the system
            foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages)
            {
                langs.Add(lang);
                //if (lang.Culture.EnglishName.ToLower().StartsWith(inputName))                  return lang;
            }

            // get the current lang
            currentLang = langs.IndexOf(InputLanguage.CurrentInputLanguage);
        }


        public void Next()
        {
            //currentLang++;
            //if (currentLang > langs.Count() - 1) currentLang = 0;
            SetCurrentInputLanguage(langs[currentLang]);
            //ActivateKeyboardLayout(1, 0x8);
            return;
        }


        // process-wide switch
        private void SetCurrentInputLanguage(InputLanguage lang)
        {
            InputLanguage.CurrentInputLanguage = lang;
        }



        // system-wide switch
        //[DllImport("user32.dll")]
        //private static extern int ActivateKeyboardLayout(int hkl, uint flags);

        public enum HKL { HKL_PREV, HKL_NEXT }
        /// <summary>Sets the input locale identifier (formerly called the keyboard layout handle) for the calling thread or the current process. The input locale identifier specifies a locale as well as the physical layout of the keyboard</summary>
        /// <param name="hkl">Input locale identifier to be activated.</param>
        /// <param name="Flags">Specifies how the input locale identifier is to be activated.</param>
        /// <returns>The return value is of type HKL. If the function succeeds, the return value is the previous input locale identifier. Otherwise, it is zero</returns>
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern HKL ActivateKeyboardLayout(HKL hkl, uint Flags);


    }
}
