using Assets.Scripts.Core.ViewModels;
using InventorySystem.Runtime.Scripts.Core.Models.Interfaces;
using InventorySystem.Runtime.Scripts.Core.ViewModels.Inventory;
using InventorySystem.Runtime.Scripts.Inventory.Tooltip;
using UnityEngine;
using UnityInventorySystem.Inventory;
using Zenject;

namespace UnityInventorySystem.Installers
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
				.BindFactory<IItem, TooltipBehaviour, TooltipBehaviour.Factory>()
				.FromNewComponentOnNewPrefabResource("Prefabs/TooltipPanel");
			
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