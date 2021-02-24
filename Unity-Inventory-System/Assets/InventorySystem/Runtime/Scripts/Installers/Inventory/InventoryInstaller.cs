using InventorySystem.Runtime.Scripts.Core.Models.Interfaces;
using InventorySystem.Runtime.Scripts.Core.ViewModels.Inventory;
using InventorySystem.Runtime.Scripts.Inventory;
using InventorySystem.Runtime.Scripts.Inventory.Item;
using InventorySystem.Runtime.Scripts.Inventory.Tooltip;
using UnityEngine;
using Zenject;

namespace InventorySystem.Runtime.Scripts.Installers.Inventory
{
	public class InventoryInstaller : Installer<int, InventoryInstaller>
	{
		private readonly int _slotsCount;
		public InventoryInstaller(
			[InjectOptional]
			int slotsCount)
		{
			_slotsCount = slotsCount;
		}
		
		public override void InstallBindings()
		{
			SlotsContainerInstaller.Install(Container);
			ItemsContainerInstaller.Install(Container);

			Container
				.BindFactory<IItemFacade, TooltipView, TooltipView.Factory>()
				.FromNewComponentOnNewPrefabResource("Prefabs/TooltipPanel");

			Container
				.Bind<TooltipBehavior>()
				.ToSelf()
				.AsSingle();
			
			Container
				.Bind<InventoryEndDragBehaviour>()
				.ToSelf()
				.AsSingle();
			
			Container
				.Bind<Transform>()
				.FromComponentOnRoot();

			Container
				.BindInstance(_slotsCount)
				.WhenInjectedInto<InventoryViewModel>();
			
			Container
				.BindInterfacesAndSelfTo<InventoryViewModel>()
				.AsSingle();
			
			Container
				.BindInterfacesAndSelfTo<InventoryFacade>()
				.AsSingle();

			Container
				.BindInterfacesAndSelfTo<InventoryBehaviour>()
				.AsSingle();
		}
	}
}