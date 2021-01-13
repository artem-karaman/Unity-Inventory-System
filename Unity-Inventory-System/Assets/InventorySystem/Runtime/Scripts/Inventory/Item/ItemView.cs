using InventorySystem.Runtime.Scripts.Core.Models.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem.Runtime.Scripts.Inventory.Item
{
	public class ItemView 
	{
		private readonly Transform _itemTransform;

		public ItemView(Transform itemTransform)
		{
			_itemTransform = itemTransform;
		}
		
		public void PrepareItem(IItem item)
		{
			var image = _itemTransform.gameObject.GetComponent<Image>();
			image.color = item.Color;
		}
	}
}