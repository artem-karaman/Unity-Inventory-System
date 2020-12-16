using InventorySystem.Runtime.Scripts.Core.ViewModels.Inventory;
using InventorySystem.Runtime.Scripts.Inventory.Item;
using InventorySystem.Runtime.Scripts.Models;
using Zenject;

namespace InventorySystem.Runtime.Scripts.Installers.Inventory
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