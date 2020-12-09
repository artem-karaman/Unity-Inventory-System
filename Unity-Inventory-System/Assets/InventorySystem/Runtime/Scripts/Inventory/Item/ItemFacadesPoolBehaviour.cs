using System.Collections.Generic;
using System.Linq;
using InventorySystem.Runtime.Scripts.Core.Models.Interfaces;
using UnityEngine;
using UnityInventorySystem;

namespace InventorySystem.Runtime.Scripts.Inventory.Item
{
	public class ItemFacadesPoolBehaviour
	{
		private readonly ItemFacade.Factory _itemFacadeFactory;
		private readonly List<IItemFacade> _items = new List<IItemFacade>();

		public ItemFacadesPoolBehaviour(
			ItemFacade.Factory itemFacadeFactory)
		{
			_itemFacadeFactory = itemFacadeFactory;
		}

		public List<IItemFacade> Items => _items;
		
		public ItemFacade AddItem(Transform parent, IItem itemData)
		{
			var item = _itemFacadeFactory.Create();
			item.SetItem(itemData);
			item.transform.SetParent(parent, false);
			_items.Add(item);

			return item;
		}

		public void RemoveItem()
		{
			if (!_items.Any()) return;
			var item = _items[0];
			item.Dispose();
			_items.Remove(item);
		}

		public void RemoveItem(IItemFacade item)
		{
			item.Dispose();
			_items.Remove(item);
		}
	}
}