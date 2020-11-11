using UniRx;
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
			_inventoryFacade.AddItem(new HandItem());
			
			_inventoryFacade.AddItem(new BodyItem());
			_inventoryFacade.AddItem(new BodyItem());
			
			_inventoryFacade.AddItem(new LegItem());
			_inventoryFacade.AddItem(new LegItem());
			_inventoryFacade.AddItem(new LegItem());
			
			_inventoryFacade.AddItem(new CardItem());
			_inventoryFacade.AddItem(new CardItem());
			_inventoryFacade.AddItem(new CardItem());
			_inventoryFacade.AddItem(new CardItem());
			
			_inventoryFacade.AddItem(new OtherItem());
			_inventoryFacade.AddItem(new OtherItem());
			_inventoryFacade.AddItem(new OtherItem());
			_inventoryFacade.AddItem(new OtherItem());
			_inventoryFacade.AddItem(new OtherItem());
		}

		private void SubscribeComponents()
		{
			var handItemsButton = _mainSceneUIManager.HandItemsButton;
			var bodyItemsButton = _mainSceneUIManager.BodyItemsButton;
			var legItemsButton = _mainSceneUIManager.LegItemsButton;
			var cardItemsButton = _mainSceneUIManager.CardItemsButton;
			var otherItemsButton = _mainSceneUIManager.OtherItemsButton;

			handItemsButton
				.OnClickAsObservable()
				.Subscribe(_ => _inventoryFacade.FilterItems<IHandItem>())
				.AddTo(Disposables);
			
			bodyItemsButton
				.OnClickAsObservable()
				.Subscribe(_ => _inventoryFacade.FilterItems<IBodyItem>())
				.AddTo(Disposables);
			
			legItemsButton
				.OnClickAsObservable()
				.Subscribe(_ => _inventoryFacade.FilterItems<ILegItem>())
				.AddTo(Disposables);
			
			cardItemsButton
				.OnClickAsObservable()
				.Subscribe(_ => _inventoryFacade.FilterItems<ICardItem>())
				.AddTo(Disposables);
			
			otherItemsButton
				.OnClickAsObservable()
				.Subscribe(_ => _inventoryFacade.FilterItems<IOtherItem>())
				.AddTo(Disposables);
		}

	}
}
