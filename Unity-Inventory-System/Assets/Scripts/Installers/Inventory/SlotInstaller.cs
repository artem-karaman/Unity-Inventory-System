using Assets.Scripts.Core.ViewModels;
using UnityEngine;
using UnityInventorySystem.Inventory;
using Zenject;

namespace UnityInventorySystem.Installers
{
	public class SlotInstaller : Installer<SlotInstaller>
	{
		public override void InstallBindings()
		{
			Container
				.Bind<Transform>()
				.FromComponentOnRoot();

			Container
				.BindInterfacesAndSelfTo<SlotFacade>()
				.AsSingle();
			
			Container
				.BindInterfacesAndSelfTo<SlotViewModel>()
				.AsSingle();
			
			Container
				.BindInterfacesAndSelfTo<SlotView>()
				.AsSingle();

			Container
				.BindInterfacesAndSelfTo<SlotBehaviour>()
				.AsSingle();
		}
	}
}