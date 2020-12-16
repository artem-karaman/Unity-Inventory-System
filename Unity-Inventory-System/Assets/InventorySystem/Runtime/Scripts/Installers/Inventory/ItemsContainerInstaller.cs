using InventorySystem.Runtime.Scripts.Inventory.Item;
using UnityInventorySystem.Installers;
using Zenject;

namespace InventorySystem.Runtime.Scripts.Installers.Inventory
{
	public class ItemsContainerInstaller : Installer<ItemsContainerInstaller>
	{
		public override void InstallBindings()
		{
			Container
				.BindInterfacesAndSelfTo<ItemFacadesPoolBehaviour>()
				.AsSingle();
			
			Container
				.BindFactory<ItemFacade, ItemFacade.Factory>()
				.FromMonoPoolableMemoryPool(x => 
					x
						.WithInitialSize(3)
						.FromSubContainerResolve()
						.ByNewPrefabResourceInstaller<ItemInstaller>("Prefabs/Item")
						.WithGameObjectName("Item"));
		}
	}
}