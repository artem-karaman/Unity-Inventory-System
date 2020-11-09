using UnityInventorySystem.Inventory;
using UnityInventorySystem.Presenters.Base;
using Zenject;

namespace UnityInventorySystem.Presenters
{
	public class HorizontalInventoryPresenter : BasePresenter, IInitializable
	{
		private readonly InventoryFacade.Factory _inventoryFacadeFactory;
		private InventoryFacade _inventoryFacade;

		private readonly HotBarFacade.Factory _hotBarFacadeFactory;
		private HotBarFacade _hotBarFacade;

		public HorizontalInventoryPresenter(
			InventoryFacade.Factory inventoryFacadeFactory,
			HotBarFacade.Factory hotBarFacadeFactory)
		{
			_inventoryFacadeFactory = inventoryFacadeFactory;
			_hotBarFacadeFactory = hotBarFacadeFactory;
		}
		
		public void Initialize()
		{
			if (_inventoryFacade == null)
			{
				_inventoryFacade = _inventoryFacadeFactory.Create(20);
			}

			if (_hotBarFacade == null)
			{
				_hotBarFacade = _hotBarFacadeFactory.Create(2);
			}
		}
	}
}