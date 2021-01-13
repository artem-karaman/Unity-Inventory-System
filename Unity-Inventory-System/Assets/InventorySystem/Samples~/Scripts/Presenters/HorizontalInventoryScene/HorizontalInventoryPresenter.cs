using InventorySystem.Runtime.Scripts.Inventory;
using InventorySystem.Runtime.Scripts.Presenters.Base;
using Zenject;

namespace InventorySystem.Samples.Scripts.Presenters.HorizontalInventoryScene
{
	public class HorizontalInventoryPresenter : BasePresenter, IInitializable
	{
		private readonly InventoryFacade.Factory _inventoryFacadeFactory;
		private InventoryFacade _inventory;

		private readonly HotBarFacade.Factory _hotBarFacadeFactory;
		private HotBarFacade _hotBar;

		public HorizontalInventoryPresenter(
			InventoryFacade.Factory inventoryFacadeFactory,
			HotBarFacade.Factory hotBarFacadeFactory)
		{
			_inventoryFacadeFactory = inventoryFacadeFactory;
			_hotBarFacadeFactory = hotBarFacadeFactory;
		}
		
		public void Initialize()
		{
			if (_inventory == null)
			{
				_inventory = _inventoryFacadeFactory.Create(20);
			}

			if (_hotBar == null)
			{
				_hotBar = _hotBarFacadeFactory.Create(3);
			}
		}
	}
}