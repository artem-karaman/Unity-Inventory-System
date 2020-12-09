using UnityEngine;
using UnityInventorySystem;
using UnityInventorySystem.Inventory;
using Zenject;

namespace InventorySystem.Runtime.Scripts.Inventory.Slot
{
	public class SlotFacade : MonoBehaviour, IPoolable<IMemoryPool>, ISlotFacade
	{
		private SlotBehaviour _slotBehaviour;
		private IMemoryPool _memoryPool;

		[Inject]
		void Construct(SlotBehaviour slotBehaviour)
		{
			_slotBehaviour = slotBehaviour;
		}

		public Transform Transform => transform;

		public void Dispose() => _memoryPool?.Despawn(this);

		public void AddItemToSlot(IItemFacade item)
		{
			_slotBehaviour.AddItem(item);
		}

		public bool Empty => _slotBehaviour.Empty;
		
		public bool Selected => _slotBehaviour.Selected;

		public void SetEmpty() => _slotBehaviour.ClearStack();

		public int ItemsCount() => _slotBehaviour.ItemsCount;

		public void SetSelected(bool value) => _slotBehaviour.SetSelected(value);

		public void RemoveItem() => _slotBehaviour.RemoveItem();
		public IItemFacade Item { get; }
		
		#region PoolBehaviour
		public void OnDespawned() => _memoryPool = null;
		public void OnSpawned(IMemoryPool memoryPool) => _memoryPool = memoryPool;

		#endregion

		public class Factory : PlaceholderFactory<SlotFacade> { }
	}
}