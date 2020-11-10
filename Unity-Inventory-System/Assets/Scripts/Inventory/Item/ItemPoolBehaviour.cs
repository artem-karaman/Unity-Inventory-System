using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace UnityInventorySystem.Inventory
{
	public class ItemPoolBehaviour
	{
		private readonly ItemBehaviour.Factory _itemBehaviourFactory;
		private readonly List<ItemBehaviour> _items = new List<ItemBehaviour>();

		public ItemPoolBehaviour(
			ItemBehaviour.Factory itemBehaviourFactory)
		{
			_itemBehaviourFactory = itemBehaviourFactory;
		}

		public List<ItemBehaviour> Items => _items;
		
		public void AddItem(Transform parent, Item itemData)
		{
			var item = _itemBehaviourFactory.Create(itemData);
			item.transform.SetParent(parent, false);
			_items.Add(item);
		}

		public void RemoveItem()
		{
			if (!_items.Any()) return;
			var item = _items[0];
			item.Dispose();
			_items.Remove(item);
		}
	}
}