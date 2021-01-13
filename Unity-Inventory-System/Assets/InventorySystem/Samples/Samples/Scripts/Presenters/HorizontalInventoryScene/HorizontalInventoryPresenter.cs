using System.Collections.Generic;
using InventorySystem.Runtime.Scripts.Core.Models.Interfaces;
using InventorySystem.Runtime.Scripts.Inventory;
using InventorySystem.Runtime.Scripts.Presenters.Base;
using InventorySystem.Samples.Scripts.Models;
using Samples.Scripts.Models;
using Zenject;

namespace Samples.Scripts.Presenters.HorizontalInventoryScene
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
			if (_hotBar == null)
			{
				_hotBar = _hotBarFacadeFactory.Create(3);
			}
			
			if (_inventory == null)
			{
				_inventory = _inventoryFacadeFactory.Create(20);
			}

			FillInventory();
		}

		private void FillInventory()
		{
			_inventory.AddItems(
				new List<IItem>()
				{
					new HandItem(),

					new BodyItem(),
					new BodyItem(),

					new LegItem(),
					new LegItem(),
					new LegItem(),

					new CardItem(),
					new CardItem(),
					new CardItem(),
					new CardItem(),

					new OtherItem(),
					new OtherItem(),
					new OtherItem(),
					new OtherItem()
				});
		}
	}
}