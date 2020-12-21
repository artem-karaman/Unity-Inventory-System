using InventorySystem.Runtime.Scripts.Core.Models.Interfaces;
using InventorySystem.Runtime.Scripts.Presenters.Base;
using UniRx;
using UnityInventorySystem.Inventory;
using UnityInventorySystem.Managers;
using Zenject;

namespace UnityInventorySystem.Presenters
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
			_inventory.AddItem(new HandItem());
			
			_inventory.AddItem(new BodyItem());
			_inventory.AddItem(new BodyItem());
			
			_inventory.AddItem(new LegItem());
			_inventory.AddItem(new LegItem());
			_inventory.AddItem(new LegItem());
			
			_inventory.AddItem(new CardItem());
			_inventory.AddItem(new CardItem());
			_inventory.AddItem(new CardItem());
			_inventory.AddItem(new CardItem());
			
			_inventory.AddItem(new OtherItem());
			_inventory.AddItem(new OtherItem());
			_inventory.AddItem(new OtherItem());
			_inventory.AddItem(new OtherItem());
			_inventory.AddItem(new OtherItem());
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
