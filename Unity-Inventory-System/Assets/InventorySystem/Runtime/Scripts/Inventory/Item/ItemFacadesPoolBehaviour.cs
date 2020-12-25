using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using InventorySystem.Runtime.Scripts.Core.Models.Interfaces;
using InventorySystem.Runtime.Scripts.Core.ViewModels.Inventory;
using InventorySystem.Runtime.Scripts.Presenters.Base;
using UniRx;
using UnityEngine;
using Zenject;

namespace InventorySystem.Runtime.Scripts.Inventory.Item
{
	public class ItemFacadesPoolBehaviour : BasePresenter, IInitializable
	{
		private readonly ItemFacade.Factory _itemFacadeFactory;
		private readonly List<IItemFacade> _items = new List<IItemFacade>();
		private readonly InventoryViewModel _inventoryViewModel;

		public ItemFacadesPoolBehaviour(
			ItemFacade.Factory itemFacadeFactory,
			InventoryViewModel inventoryViewModel)
		{
			_itemFacadeFactory = itemFacadeFactory;
			_inventoryViewModel = inventoryViewModel;
		}

		public void Initialize()
		{
			_inventoryViewModel
				.Items
				.ObserveReset()
				.Subscribe(async _ => await RemoveAllItems())
				.AddTo(Disposables);

			_inventoryViewModel
				.Items
				.ObserveRemove()
				.Subscribe(value =>
				{
					RemoveItem(value.Value);
				})
				.AddTo(Disposables);
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

		public void RemoveItem(IItemFacade item)
		{
			_ = item ?? throw new ArgumentNullException(nameof(item));
			
			item.Dispose();
			_items.Remove(item);
		}

		private void RemoveItem(IItem item)
		{
			var resultItem = _items.FirstOrDefault(i => i.Item.Equals(item));

			if (resultItem == null) return;
			resultItem.Dispose();
			_items.Remove(resultItem);
		}

		private async UniTask RemoveAllItems()
		{
			if (!_items.Any()) return;

			await RemoveItems();
		}

		private UniTask RemoveItems()
		{
			foreach (var item in _items)
			{
				item.Dispose();
			}

			_items.Clear();

			return UniTask.CompletedTask;
		}
	}
}