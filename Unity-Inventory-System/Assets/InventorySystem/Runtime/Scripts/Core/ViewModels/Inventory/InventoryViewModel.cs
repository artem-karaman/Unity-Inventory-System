using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityInventorySystem;

namespace Assets.Scripts.Core.ViewModels
{
	public class InventoryViewModel
	{
		private readonly int _slotsCount;
		private ISlotFacade _selectedSlot;
		public InventoryViewModel(
			int slotsCount)
		{
			_slotsCount = slotsCount;

			SlotsCount = new ReactiveProperty<int>(slotsCount);

			InventoryItems = new List<ISlotFacade>();
		}

		public IReactiveProperty<int> SlotsCount;

		public List<ISlotFacade> InventoryItems { get; }

		public IEnumerable<IItem> FilteredItems<T>()
			where T : IItem
		{
			_selectedSlot?.SetSelected(false);

			return InventoryItems
				.Where(i => !i.Empty && i.Item.Item is T)
				.Select(i => i.Item.Item)
				.ToArray();
		}
			

		public void SetSelectedSlot(ISlotFacade slot)
		{
			if (_selectedSlot != null)
			{
				if (ReferenceEquals(_selectedSlot, slot)) return;
				
				_selectedSlot.SetSelected(false);
				_selectedSlot = slot;
			}
			else
			{
				_selectedSlot = slot;
			}
		}

		public void RemoveItem()
		{
			if(_selectedSlot.Selected)
				_selectedSlot.RemoveItem();
		}
	}
}
