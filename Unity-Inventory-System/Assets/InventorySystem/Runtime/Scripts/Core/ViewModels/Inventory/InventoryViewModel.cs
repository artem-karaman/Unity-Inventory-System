using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityInventorySystem;

namespace Assets.Scripts.Core.ViewModels
{
	public class InventoryViewModel
	{
		private readonly int _slotsCount;
		private int _totalItemsInRowOrColumn = 4;
		private ISlotFacade _selectedSlot;
		public InventoryViewModel(int slotsCount)
		{
			_slotsCount = slotsCount;

			SlotsCount = new ReactiveProperty<int>(slotsCount);

			InventoryItems = new ReactiveCollection<IItem>();
		}

		public IReactiveProperty<int> SlotsCount;

		public ReactiveCollection<IItem> InventoryItems { get; }

		public IEnumerable<IItem> FilteredItems<T>()
			where T : IItem
		{
			_selectedSlot?.SetSelected(false);
			
			return InventoryItems.Where(i => i is T);
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
		
		private int CalculateSlotsCountWithFullRow()
		{
			var modulo = _slotsCount % _totalItemsInRowOrColumn;

			return _slotsCount % 4 == 0 ? _slotsCount : _slotsCount + (_totalItemsInRowOrColumn - modulo);
		}
	}
}
