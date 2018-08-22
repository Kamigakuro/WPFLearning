using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Models
{
    public static class ThemeSwither
    {
        public static void ChangeDark(bool isDark)
        {
            new MaterialDesignThemes.Wpf.PaletteHelper().SetLightDark(isDark);
        }
    }
}
