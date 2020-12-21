using InventorySystem.Runtime.Scripts.Core.Models.Interfaces;
using UnityEngine;

namespace InventorySystem.Runtime.Scripts.Core.Models.Decorators
{
	public class ItemCountDecorator : IItem
	{
		private readonly Item _item;
		private readonly int _count;

		public ItemCountDecorator(
			Item item, 
			int count)
		{
			_item = item;
			_count = count;
		}

		public string Title => _item.Title;
		public string Description => _item.Description;
		public Sprite ItemSprite => _item.ItemSprite;
		public Color Color => _item.Color;
		public int MaxStack => _item.MaxStack;
		
		public int Count => _item.Count + _count;
	}
}