using Assets.Scripts.Core.ViewModels;
using UnityInventorySystem.Inventory;
using Zenject;

namespace UnityInventorySystem.Installers
{
	public class MainInventoryInstaller : Installer<MainInventoryInstaller>
	{
		public override void InstallBindings()
		{
			Container
				.BindInterfacesAndSelfTo<InventoryBehaviour>()
				.AsSingle();
			Container
				.BindInterfacesAndSelfTo<InventoryViewModel>()
				.AsSingle();
		}
	}
}