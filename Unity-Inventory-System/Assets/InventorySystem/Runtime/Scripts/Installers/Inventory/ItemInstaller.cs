using Assets.Scripts.Core.ViewModels;
using InventorySystem.Runtime.Scripts.Inventory.Item;
using InventorySystem.Runtime.Scripts.Models;
using UnityInventorySystem.Inventory;
using Zenject;

namespace UnityInventorySystem.Installers
{
	public class ItemInstaller : Installer<ItemInstaller>
	{
		public override void InstallBindings()
		{
			Container
				.Bind<ItemFacade>()
				.FromNewComponentOnRoot()
				.AsSingle();

			Container
				.BindInterfacesAndSelfTo<ItemViewModel>()
				.AsSingle();

			Container
				.BindInterfacesAndSelfTo<ItemView>()
				.AsSingle();

			Container
				.BindInterfacesAndSelfTo<ItemBehaviour>()
				.AsSingle();

			Container
				.BindFactory<ItemDragData, ItemEndDragBehaviour, ItemEndDragBehaviour.Factory>()
				.AsSingle();
		}
	}
}