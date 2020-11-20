using System;
using UnityEditor;
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

		protected Item()
			: this("other", "default item", Color.gray)
		{
		}

		protected Item(Color color)
			: this("other", "default", color)
		{
		}

		protected Item(Color color, int maxStack)
			: this("", "", color, maxStack)
		{
		}

		protected Item(
			string title,
			string description,
			Sprite itemSprite)
			: this(title, description, Color.gray)
		{
			ItemSprite = itemSprite;
		}

		protected Item(string title, string description, Color color, int maxStack = 1)
		{
			Title = title;
			Description = description;
			Color = color;
			MaxStack = maxStack;
		}


		public string Title { get; }
		public string Description { get; }
		public Sprite ItemSprite { get; }
		public Color Color { get; }
		public int MaxStack { get; }
	}
}