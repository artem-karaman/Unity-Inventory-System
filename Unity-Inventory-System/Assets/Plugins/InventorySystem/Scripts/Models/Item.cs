using System;
using UnityEngine;

namespace UnityInventorySystem
{
	[Serializable]
	public abstract class Item
	{
		private string _titile;
		private string _description;
		private Sprite _itemSprite;
		private Color _color;
		private int _maxStack;

		public Item() 
			: this("other","default item", Color.gray)
		{
		}
		
		public Item(
			string title, 
			string description, 
			Sprite itemSprite)
		: this(title, description, Color.gray)
		{
			ItemSprite = itemSprite;
		}

		public Item(string title, string description, Color color, int maxStack = 1)
		{
			Title = title;
			Description = description;
			Color = color;
			MaxStack = maxStack;
		}

		public Item(Color color)
			: this("other", "default", color)
		{
			
		}
			

		public string Title { get; }
		public string Description { get;  }
		public Sprite ItemSprite { get;  }
		public Color Color { get; }
		public int MaxStack { get; }
	}
}