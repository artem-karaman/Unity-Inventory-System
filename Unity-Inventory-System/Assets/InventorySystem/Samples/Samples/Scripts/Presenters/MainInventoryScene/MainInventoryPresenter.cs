using System.Collections.Generic;
using InventorySystem.Runtime.Scripts.Core.Models.Interfaces;
using InventorySystem.Runtime.Scripts.Inventory;
using InventorySystem.Runtime.Scripts.Presenters.Base;
using InventorySystem.Samples.Scripts.Managers;
using InventorySystem.Samples.Scripts.Models;
using InventorySystem.Samples.Scripts.Models.Interfaces;
using Samples.Scripts.Models;
using UniRx;
using Zenject;

namespace InventorySystem.Samples.Scripts.Presenters.MainInventoryScene
{
	public class MainInventoryPresenter : BasePresenter, IInitializable
	{
		private readonly InventoryFacade.Factory _inventoryFacadeFactory;
		private readonly MainSceneUIManager _mainSceneUIManager;
		
		private InventoryFacade _inventory;
		public MainInventoryPresenter(
			InventoryFacade.Factory inventoryFacadeFactory,
			MainSceneUIManager mainSceneUIManager)
		{
			_inventoryFacadeFactory = inventoryFacadeFactory;
			_mainSceneUIManager = mainSceneUIManager;
		}
		
		public void Initialize()
		{
			if (_inventory == null)
			{
				_inventory = _inventoryFacadeFactory.Create(20);
			}

			FillInventory();
			SubscribeComponents();
		}

		private void FillInventory()
		{
			_inventory.AddItems(new List<IItem>()
			{
				new HandItem(),
				
				new BodyItem("BodyItem","This is body item to protect own body"),
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

		private void SubscribeComponents()
		{
			var handItemsButton = _mainSceneUIManager.HandItemsButton;
			var bodyItemsButton = _mainSceneUIManager.BodyItemsButton;
			var legItemsButton = _mainSceneUIManager.LegItemsButton;
			var cardItemsButton = _mainSceneUIManager.CardItemsButton;
			var otherItemsButton = _mainSceneUIManager.OtherItemsButton;
			var allItemsButton = _mainSceneUIManager.AllItemsButton;

			var deleteSelectedItemButton = _mainSceneUIManager.DeleteSelectedItemButton;
			var separateItemsButton = _mainSceneUIManager.SeparateItemsButton;

			handItemsButton
				.OnClickAsObservable()
				.Subscribe(_ => _inventory.FilterItems<IHandItem>())
				.AddTo(Disposables);
			
			bodyItemsButton
				.OnClickAsObservable()
				.Subscribe(_ => _inventory.FilterItems<IBodyItem>())
				.AddTo(Disposables);
			
			legItemsButton
				.OnClickAsObservable()
				.Subscribe(_ => _inventory.FilterItems<ILegItem>())
				.AddTo(Disposables);
			
			cardItemsButton
				.OnClickAsObservable()
				.Subscribe(_ => _inventory.FilterItems<ICardItem>())
				.AddTo(Disposables);

			otherItemsButton
				.OnClickAsObservable()
				.Subscribe(_ => _inventory.FilterItems<IOtherItem>())
				.AddTo(Disposables);

			allItemsButton
				.OnClickAsObservable()
				.Subscribe(_ => _inventory.FilterItems<IItem>())
				.AddTo(Disposables);
			
			deleteSelectedItemButton
				.OnClickAsObservable()
				.Subscribe(_ =>
				{
					_inventory.RemoveSelectedItem();
				})
				.AddTo(Disposables);
		}
	}
}
