using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityInventorySystem.Inventory
{
	public class ItemFacadesPoolBehaviour
	{
		private readonly ItemFacade.Factory _itemFacadeFactory;
		private readonly List<ItemFacade> _items = new List<ItemFacade>();

		public ItemFacadesPoolBehaviour(
			ItemFacade.Factory itemFacadeFactory)
		{
			_itemFacadeFactory = itemFacadeFactory;
		}

		public List<ItemFacade> Items => _items;
		
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

		public void RemoveItem(ItemFacade item)
		{
			item.Dispose();
			_items.Remove(item);
		}
	}
}