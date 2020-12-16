using System;
using System.Collections.Generic;
using System.Linq;
using InventorySystem.Runtime.Scripts.Core.Models.Interfaces;
using InventorySystem.Runtime.Scripts.Core.ViewModels.Inventory;
using InventorySystem.Runtime.Scripts.Presenters.Base;
using UniRx;
using UnityEngine;
using Zenject;

namespace InventorySystem.Runtime.Scripts.Inventory.Slot
{
	public class SlotFacadesPoolBehaviour : BasePresenter, IInitializable
	{
		private readonly SlotFacade.Factory _slotFacadeFactory;
		private readonly InventoryViewModel _inventoryViewModel;
		private List<ISlotFacade> _slots = new List<ISlotFacade>();
		private Transform _parent;
		
		public SlotFacadesPoolBehaviour(
			SlotFacade.Factory slotFacadeFactory,
			InventoryViewModel inventoryViewModel)
		{
			_slotFacadeFactory = slotFacadeFactory;
			_inventoryViewModel = inventoryViewModel;
		}

		public void Initialize()
		{
			_inventoryViewModel
				.Items
				.ObserveReset()
				.Subscribe(_ => ClearSlots())
				.AddTo(Disposables);
		}

		public void SetParent(Transform parent)
		{
			_ = parent ? parent : throw new ArgumentNullException(nameof(parent));
			_parent = parent;
		}

		public void AddSlot()
		{
			var slot = _slotFacadeFactory.Create();
			_inventoryViewModel.InventorySlots.Add(slot);
			slot.Transform.SetParent(_parent);
			_slots.Add(slot);
		}

		public ISlotFacade FindEmptySlot() => _slots.First(s => s.Empty);

		public void ClearSlots() => _slots.ForEach(s => s.SetEmpty());
	}
}