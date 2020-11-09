using Assets.Scripts.Core.ViewModels;
using UnityEngine;
using Zenject;

namespace UnityInventorySystem.Inventory
{
	public class InventoryBehaviour : MonoBehaviour
	{
		private InventoryViewModel _inventoryViewModel;
		
		[Inject]
		void Construct(
			InventoryViewModel inventoryViewModel)
		{
			_inventoryViewModel = inventoryViewModel;
		}
		
		void Start()
		{
		
		}
		
		public class Factory : PlaceholderFactory<InventoryViewModel, InventoryBehaviour>{}
	}
}
