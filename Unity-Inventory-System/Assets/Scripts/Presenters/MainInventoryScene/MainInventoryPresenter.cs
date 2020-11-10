using UniRx;
using UnityEngine;
using UnityInventorySystem.Inventory;
using UnityInventorySystem.Managers;
using UnityInventorySystem.Presenters.Base;
using Zenject;

namespace UnityInventorySystem.Presenters
{
	public class MainInventoryPresenter : BasePresenter, IInitializable
	{
		private readonly InventoryFacade.Factory _inventoryFacadeFactory;
		private readonly MainSceneUIManager _mainSceneUIManager;
		
		private InventoryFacade _inventoryFacade;
		public MainInventoryPresenter(
			InventoryFacade.Factory inventoryFacadeFactory,
			MainSceneUIManager mainSceneUIManager)
		{
			_inventoryFacadeFactory = inventoryFacadeFactory;
			_mainSceneUIManager = mainSceneUIManager;
		}
		
		public void Initialize()
		{
			if (_inventoryFacade == null)
			{
				_inventoryFacade = _inventoryFacadeFactory.Create(35);
			}

			FillInventory();
			SubscribeComponents();
		}

		private void FillInventory()
		{
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

		private void SubscribeComponents()
		{
			var clearInventoryButton = _mainSceneUIManager.ClearInventoryButton;
			clearInventoryButton
				.OnClickAsObservable()
				.Subscribe(_ =>
				{
					_inventoryFacade.ClearItems();
				})
				.AddTo(Disposables);
		}
	}
}
