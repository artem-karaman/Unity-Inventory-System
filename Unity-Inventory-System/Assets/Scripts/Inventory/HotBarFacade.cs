using Zenject;

namespace UnityInventorySystem.Inventory
{
	public class HotBarFacade
	{
		private readonly InventoryBehaviour _inventory;

		public HotBarFacade(InventoryBehaviour inventory)
		{
			_inventory = inventory;
		}
		
		
		
		public class Factory : PlaceholderFactory<int, HotBarFacade> { }
	}
}