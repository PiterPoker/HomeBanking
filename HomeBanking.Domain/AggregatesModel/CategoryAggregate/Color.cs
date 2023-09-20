using HomeBanking.Domain.Exceptions;
using HomeBanking.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeBanking.Domain.AggregatesModel.CategoryAggregate
{
    public class Color
        : Enumeration
    {
        public static Color None = new Color(1, nameof(None).ToLowerInvariant());
        public static Color Aquamarine = new Color(2, nameof(Aquamarine).ToLowerInvariant());
        public static Color AntiqueWhite = new Color(3, nameof(AntiqueWhite).ToLowerInvariant());
        public static Color AliceBlue = new Color(4, nameof(AliceBlue).ToLowerInvariant());
        public static Color Azure = new Color(5, nameof(Azure).ToLowerInvariant());
        public static Color Blue = new Color(6, nameof(Blue).ToLowerInvariant());
        public static Color Crimson = new Color(7, nameof(Crimson).ToLowerInvariant());
        public static Color Green = new Color(8, nameof(Green).ToLowerInvariant());
        public static Color Red = new Color(9, nameof(Red).ToLowerInvariant());
        public static Color Yellow = new Color(10, nameof(Yellow).ToLowerInvariant());

        public Color(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<Color> List() =>
            new[] { None, Aquamarine, AntiqueWhite, AliceBlue, Azure, Blue, Crimson, Green, Red, Yellow };

        public static Color FromName(string name)
        {
            var color = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (color == null)
            {
                throw new HomeBankingDomainException($"Possible values for Color: {String.Join(",", List().Select(s => s.Name))}");
            }

            return color;
        }

        public static Color From(int id)
        {
            var color = List().SingleOrDefault(s => s.Id == id);

            if (color == null)
            {
                throw new HomeBankingDomainException($"Possible values for Color: {String.Join(",", List().Select(s => s.Name))}");
            }

            return color;
        }
    }
}
