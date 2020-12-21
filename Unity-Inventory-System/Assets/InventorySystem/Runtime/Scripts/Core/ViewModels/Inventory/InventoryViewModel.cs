using System;
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
		private readonly CompositeDisposable _disposables = new CompositeDisposable();
		private readonly int _slotsCount;
		private ISlotFacade _selectedSlot;
		private IReactiveCollection<IItem> _originalCollection;
		public InventoryViewModel(
			int slotsCount)
		{
			_slotsCount = slotsCount;
			SlotsCount = new ReactiveProperty<int>(slotsCount);
			
			InventorySlots = new List<ISlotFacade>();
			Items = new ReactiveCollection<IItem>();
			_originalCollection = new ReactiveCollection<IItem>();
		}

		public IReactiveProperty<int> SlotsCount;
		public List<ISlotFacade> InventorySlots { get; }
		
		public IReactiveCollection<IItem> Items { get; }

		public void AddItems(IEnumerable<IItem> items)
		{
			_ = items ?? throw new ArgumentNullException(nameof(items));

			foreach (var item in items)
			{
				AddItem(item);
			}
		}

		public void AddItem(IItem item)
		{
			_ = item ?? throw new ArgumentNullException(nameof(item));
			
			_originalCollection.Add(item);
		}

		public void FilterItems<T>()
			where T : IItem
		{
			_selectedSlot = null;

			Items.Clear();
			
			var list = _originalCollection.Where(i => i is T).ToList();

			foreach (var item in list)
			{
				Items.Add(item);
			}
		}

		// public void RemoveItem()
		// {
		// 	if (_selectedSlot != null)
		// 	{
		// 		if(_selectedSlot.Selected)
		// 			_selectedSlot.ClearItems();
		// 	}
		// }

		public void MoveItem(ISlotFacade from, ISlotFacade to)
		{
			var items = from?.AllItemsInSlot;
			if (items != null)
			{
				to.AddItemsToSlot(items);
			}
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
				.AddTo(_disposables);

			_originalCollection
				.ObserveAdd()
				.Subscribe(value =>
				{
					Items.Add(value.Value);
				})
				.AddTo(_disposables);

			_originalCollection
				.ObserveRemove()
				.Subscribe(value =>
				{
					Items.Remove(value.Value);
				})
				.AddTo(_disposables);
		}

		public void LateDispose()
		{
			_disposables?.Dispose();
		}

		public void RemoveSelectedItem()
		{
			if (_selectedSlot == null) return;
			var item = _selectedSlot?.AllItemsInSlot?.First()?.Item;
			_originalCollection.Remove(item);
			_selectedSlot.ClearItems();
			_selectedSlot.SetSelected(false);
		}
	}
}
