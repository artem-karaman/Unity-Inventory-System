using Assets.Scripts.Core.ViewModels;
using UnityInventorySystem.Inventory;
using UnityInventorySystem.Presenters.Base;
using Zenject;

namespace UnityInventorySystem.Presenters
{
	public class MainInventoryPresenter : BasePresenter, IInitializable
	{
		private readonly InventoryBehaviour.Factory _inventoryBehaviourFactory;
		private readonly InventoryViewModel _inventoryViewModel;

		private InventoryBehaviour _inventory;

		public MainInventoryPresenter(
			InventoryBehaviour.Factory inventoryBehaviourFactory, 
			InventoryViewModel inventoryViewModel)
		{
			_inventoryBehaviourFactory = inventoryBehaviourFactory;
			_inventoryViewModel = inventoryViewModel;
		}
		
		public void Initialize()
		{
			if (_inventory == null)
			{
				_inventory = _inventoryBehaviourFactory.Create();
			}
		}
	}
}
