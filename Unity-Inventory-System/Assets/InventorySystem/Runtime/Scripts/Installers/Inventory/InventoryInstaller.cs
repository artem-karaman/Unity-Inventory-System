using Assets.Scripts.Core.ViewModels;
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
				.Bind<Transform>()
				.FromComponentOnRoot();

			Container
				.BindInterfacesAndSelfTo<InventoryFacade>()
				.AsSingle();
			
			Container
				.BindInterfacesAndSelfTo<InventoryBehaviour>()
				.AsSingle();

			Container
				.BindInstance(_slotsCount)
				.WhenInjectedInto<InventoryViewModel>();
			
			Container
				.BindInterfacesAndSelfTo<InventoryViewModel>()
				.AsSingle();
		}
	}
}