using Assets.Scripts.Core.ViewModels;
using UniRx;
using UnityEngine;
using Zenject;

namespace UnityInventorySystem.Inventory
{
	public class InventoryBehaviour : MonoBehaviour
	{
		private InventoryViewModel _inventoryViewModel;
		private SlotsPoolBehaviour _slotsPoolBehaviour;
		
		[Inject]
		void Construct(
			InventoryViewModel inventoryViewModel,
			SlotsPoolBehaviour slotsPoolBehaviour)
		{
			_inventoryViewModel = inventoryViewModel;
			_slotsPoolBehaviour = slotsPoolBehaviour;
		}
		
		void Start()
		{
			PrepareComponents();
			SubscribeComponents();
		}

		private void PrepareComponents()
		{
			//Due to content game object is a second child root inventory component
			var content = transform.GetChild(0).GetChild(0);
			_slotsPoolBehaviour.SetParent(content);
		}

		private void SubscribeComponents()
		{
			_inventoryViewModel
				.SlotsCount
				.AsObservable()
				.Subscribe(AddSlotsToInventory)
				.AddTo(this);
		}

		private void AddSlotsToInventory(int value)
		{
			for (int i = 0; i < value; i++)
			{
				_slotsPoolBehaviour.AddSlot();
			}
		}

		public class Factory : PlaceholderFactory<InventoryBehaviour>{}
	}
}
