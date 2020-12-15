using System.Collections.Generic;
using System.Linq;
using InventorySystem.Runtime.Scripts.Core.Messages;
using InventorySystem.Runtime.Scripts.Core.Models.Interfaces;
using UniRx;
using Zenject;

namespace InventorySystem.Runtime.Scripts.Core.ViewModels.Inventory
{
	public class InventoryViewModel : IInitializable, ILateDisposable
	{
		private readonly CompositeDisposable _compositeDisposable = new CompositeDisposable();
		private readonly int _slotsCount;
		private ISlotFacade _selectedSlot;
		private List<IItem> _originalList;
		public InventoryViewModel(
			int slotsCount)
		{
			_slotsCount = slotsCount;
			SlotsCount = new ReactiveProperty<int>(slotsCount);
			InventoryItems = new List<ISlotFacade>();

			_originalList = new List<IItem>();
		}

		public IReactiveProperty<int> SlotsCount;

		public List<ISlotFacade> InventoryItems { get; }

		public IEnumerable<IItem> FilteredItems<T>()
			where T : IItem
		{
			_selectedSlot?.SetSelected(false);

			return null;
			// InventoryItems
			// .Where(i => !i.Empty && i.Item.Item is T)
			// .Select(i => i.Item.Item)
			// .ToArray();
		}

		public void RemoveItem()
		{
			if (_selectedSlot != null)
			{
				if(_selectedSlot.Selected)
					_selectedSlot.ClearItems();
			}
		}

		public void MoveItem(ISlotFacade from, ISlotFacade to)
		{
			var items = from.AllItemsInSlot;
			to.AddItemsToSlot(items);
		}

		public void Initialize()
		{
			MessageBroker
				.Default
				.Receive<NewSlotSelectedMessage>()
				.AsObservable()
				.Subscribe(value =>
				{
					_selectedSlot = value.SelectedSlot;
				})
				.AddTo(_compositeDisposable);
		}

		public void LateDispose()
		{
			_compositeDisposable?.Dispose();
		}

		public void RemoveSelectedItem()
		{
			
		}
	}
}
