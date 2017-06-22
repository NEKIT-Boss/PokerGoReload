using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Poker.Core
{
    public class NamedColor
    {
        public string Name { get; }

        public Color Value { get; }

        public SolidColorBrush Brush { get; }

        public NamedColor(string name, Color color)
        {
            Name = name;
            Value = color;

            Brush = new SolidColorBrush(color);
        }

        public static List<NamedColor> PredefinedColors => new List<NamedColor>
        {
            new NamedColor("Red", Colors.DarkRed),
            new NamedColor("Blue", Colors.RoyalBlue),
            new NamedColor("Green", Colors.ForestGreen),
            new NamedColor("Yellow", Colors.Yellow),
        };
    }
}