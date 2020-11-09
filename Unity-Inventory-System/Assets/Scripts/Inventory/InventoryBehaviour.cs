using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core.ViewModels;
using UniRx;
using UnityEngine;
using UnityInventorySystem.Presenters.Base;
using Zenject;

namespace UnityInventorySystem.Inventory
{
	public class InventoryBehaviour : BasePresenter, IInitializable
	{
		private InventoryViewModel _inventoryViewModel;
		private SlotsPoolBehaviour _slotsPoolBehaviour;
		private ItemPoolBehaviour _itemPoolBehaviour;

		private List<SlotBehaviour> _allSlotsBehaviour;

		private Transform _transform;
		
		[Inject]
		void Construct(
			InventoryViewModel inventoryViewModel,
			SlotsPoolBehaviour slotsPoolBehaviour,
			ItemPoolBehaviour itemPoolBehaviour,
			Transform transform)
		{
			_inventoryViewModel = inventoryViewModel;
			_slotsPoolBehaviour = slotsPoolBehaviour;
			_itemPoolBehaviour = itemPoolBehaviour;
			_transform = transform;
		}

		public void Initialize()
		{
			PrepareComponents();
			SubscribeComponents();
		}

		private void PrepareComponents()
		{
			//Due to content game object is a second child root inventory component
			var content = _transform.GetChild(0).GetChild(0);
			_slotsPoolBehaviour.SetParent(content);
		}

		private void SubscribeComponents()
		{
			_inventoryViewModel
				.SlotsCount
				.AsObservable()
				.Subscribe(AddSlots)
				.AddTo(Disposables);
		}

		public bool Full => _allSlotsBehaviour.All(x => !x.Empty);

		public SlotBehaviour[] EmptySlots => _allSlotsBehaviour.Where(x => x.Empty).ToArray();

		private void AddSlots(int value)
		{
			for (var i = 0; i < value; i++)
			{
				_slotsPoolBehaviour.AddSlot();
			}

			AddItems();
		}

		private void AddItems()
		{
			_itemPoolBehaviour.AddItem(_slotsPoolBehaviour?.SlotList[0]?.transform);
		}
	}
}
