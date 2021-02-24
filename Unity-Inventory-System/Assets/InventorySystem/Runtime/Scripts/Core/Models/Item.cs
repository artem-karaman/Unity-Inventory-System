using System;
using UnityEditor.Graphs;
using UnityEngine;

namespace InventorySystem.Runtime.Scripts.Core.Models
{
	[Serializable]
	public abstract class Item
	{
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

		public string Title { get; private set; }
		public string Description { get; private set; }
		public Sprite ItemSprite { get; }
		public Color Color { get; }
		public int MaxStack { get; }
		public int Count { get; }
	}
}