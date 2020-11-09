using Zenject;

namespace UnityInventorySystem.Inventory
{
	public class InventoryFacade 
	{
		public class Factory : PlaceholderFactory<int, InventoryFacade>{}
	}
}