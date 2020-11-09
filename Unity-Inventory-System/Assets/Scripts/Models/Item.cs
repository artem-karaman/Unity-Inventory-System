using System;
using UnityEngine;

namespace UnityInventorySystem
{
	[Serializable]
	public class Item
	{
		public string Title;
		public string Description;
		public Sprite ItemSprite;

		public Item(){}
		
		public Item(string title, string description, Sprite itemSprite)
		{
			Title = title;
			Description = description;
			ItemSprite = itemSprite;
		}
	}
}