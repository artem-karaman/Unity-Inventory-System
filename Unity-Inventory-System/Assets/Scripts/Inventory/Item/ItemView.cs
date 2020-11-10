using Zenject;

namespace UnityInventorySystem.Inventory
{
	public class ItemView : IInitializable
	{
		public void Initialize()
		{
			
		}
	}
	
	public class Factory : PlaceholderFactory<ItemView>{}
}