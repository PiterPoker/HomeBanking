using HomeBanking.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBanking.Domain.AggregatesModel.CategoryAggregate
{
    public class Category
      : Entity, IAggregateRoot
    {
        private string _name;
        private int _colorId;

        public string Name { get => _name; }
        public Color Color { get => Color.From(_colorId); }
        public int ColorId { get => _colorId; }

        public Category()
        {
            _name = String.Empty;
            _colorId = Color.None.Id;
        }

        public Category(string name, Color color) : this()
        {
            _name = name;
            _colorId = color.Id;
        }

        public void SetColorById(int colorId)
        {
            _colorId = colorId;
        }

        public void SetName(string name) => _name = name;        

        public static Category NewCategory() => new Category();
    }
}
