using UnityEngine;
using UnityInventorySystem.Inventory;
using UnityInventorySystem.Presenters.Base;
using Zenject;

namespace UnityInventorySystem.Presenters
{
	public class MainInventoryPresenter : BasePresenter, IInitializable
	{
		private readonly InventoryFacade.Factory _inventoryFacadeFactory;
		private InventoryFacade _inventoryFacade;
		public MainInventoryPresenter(
			InventoryFacade.Factory inventoryFacadeFactory)
		{
			_inventoryFacadeFactory = inventoryFacadeFactory;
		}
		
		public void Initialize()
		{
			if (_inventoryFacade == null)
			{
				_inventoryFacade = _inventoryFacadeFactory.Create(35);
			}
			
			_inventoryFacade.AddItem(new Item(Color.blue));
			_inventoryFacade.AddItem(new Item(Color.blue));
			_inventoryFacade.AddItem(new Item(Color.blue));
			_inventoryFacade.AddItem(new Item(Color.blue));
			
			_inventoryFacade.AddItem(new Item(Color.green));
			_inventoryFacade.AddItem(new Item(Color.green));
			_inventoryFacade.AddItem(new Item(Color.green));
			_inventoryFacade.AddItem(new Item(Color.green));
			
			_inventoryFacade.AddItem(new Item(Color.red));
			_inventoryFacade.AddItem(new Item(Color.red));
			_inventoryFacade.AddItem(new Item(Color.red));
			_inventoryFacade.AddItem(new Item(Color.red));
			
			_inventoryFacade.AddItem(new Item(Color.yellow));
			_inventoryFacade.AddItem(new Item(Color.yellow));
			_inventoryFacade.AddItem(new Item(Color.yellow));
			_inventoryFacade.AddItem(new Item(Color.yellow));
			_inventoryFacade.AddItem(new Item(Color.yellow));
			
			_inventoryFacade.AddItem(new Item(Color.gray));
			_inventoryFacade.AddItem(new Item(Color.gray));
			_inventoryFacade.AddItem(new Item(Color.gray));
			_inventoryFacade.AddItem(new Item(Color.gray));
		}
	}
}
