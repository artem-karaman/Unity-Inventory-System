using InventorySystem.Runtime.Scripts.Inventory.Item;
using UnityInventorySystem.Inventory;
using Zenject;

namespace UnityInventorySystem.Installers
{
	public class ItemsContainerInstaller : Installer<ItemsContainerInstaller>
	{
		public override void InstallBindings()
		{
			Container
				.Bind<ItemFacadesPoolBehaviour>()
				.ToSelf()
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