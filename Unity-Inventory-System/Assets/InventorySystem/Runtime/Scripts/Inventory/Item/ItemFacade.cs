using InventorySystem.Runtime.Scripts.Core.Models.Interfaces;
using UnityEngine;
using UnityInventorySystem;
using UnityInventorySystem.Inventory;
using Zenject;

namespace InventorySystem.Runtime.Scripts.Inventory.Item
{
	public class ItemFacade : MonoBehaviour, IPoolable<IMemoryPool>, IItemFacade
	{
		private IMemoryPool _memoryPool;
		private ItemView _itemView;
		private IItem _item;

		public Transform Transform => transform;
		public IItem Item => _item;

		[Inject]
		void Construct(
			ItemView itemView)
		{
			_itemView = itemView;
		}

		public void Dispose()
		{
			_memoryPool?.Despawn(this);
		}

		public void OnDespawned()
		{
			_memoryPool = null;
		}

		public void OnSpawned(IMemoryPool p1)
		{
			_memoryPool = p1;
		}

		public void SetItem(IItem item)
		{
			_item = item;
			
			_itemView.PrepareItem(item);
		}

		public class Factory : PlaceholderFactory<ItemFacade>{}
	}
}