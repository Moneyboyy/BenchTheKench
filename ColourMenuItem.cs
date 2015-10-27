using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using Color = System.Drawing.Color;

namespace BenchTheKench
{
    static class ColourMenuItem
    {
        private static readonly Color[] Colours =
        {
            Color.BlueViolet, Color.Aqua, Color.MidnightBlue, Color.Gold,
            Color.LimeGreen, Color.Violet, Color.WhiteSmoke, Color.DarkRed
        };
        private static readonly string[] ColoursName =
        {
            "BlueViolet", "Aqua", "MidnightBlue", "Gold", "LimeGreen", "Violet", "WhiteSmoke", "DarkRed"
        };

        public static void AddColourItem(this Menu menu, string uniqueId, int defaultColour = 0)
        {
            var a = menu.Add(uniqueId, new Slider("Colour Picker: ", defaultColour, 0, Colours.Count() - 1));
            a.DisplayName = "Colour Picker: " + ColoursName[a.CurrentValue];
            a.OnValueChange += delegate { a.DisplayName = "Colour Picker: " + ColoursName[a.CurrentValue]; };
        }

        public static Color GetColour(this Menu m, string id)
        {
            var number = m[id].Cast<Slider>();
            if (number != null)
            {
                return Colours[number.CurrentValue];
            }
            return Color.AliceBlue;
        }
    }
}
