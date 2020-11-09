using Assets.Scripts.Core.ViewModels;
using UnityInventorySystem.Inventory;
using UnityInventorySystem.Presenters.Base;
using Zenject;

namespace UnityInventorySystem.Presenters
{
	public class HorizontalInventoryPresenter : BasePresenter, IInitializable
	{
		private readonly InventoryBehaviour.Factory _inventoryBehaviourFactory;

		private InventoryBehaviour _inventory;

		public HorizontalInventoryPresenter(
			InventoryBehaviour.Factory inventoryBehaviourFactory)
		{
			_inventoryBehaviourFactory = inventoryBehaviourFactory;
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