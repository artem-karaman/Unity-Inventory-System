using InventorySystem.Runtime.Scripts.Inventory.Slot;
using Zenject;

namespace InventorySystem.Runtime.Scripts.Installers.Inventory
{
	public class SlotsContainerInstaller : Installer<SlotsContainerInstaller>
	{
		public override void InstallBindings()
		{
			Container
				.BindInterfacesAndSelfTo<SlotFacadesPoolBehaviour>()
				.AsSingle();

			Container
				.BindFactory<SlotFacade, SlotFacade.Factory>()
				.FromMonoPoolableMemoryPool(x =>
					x
						.WithInitialSize(15)
						.FromSubContainerResolve()
						.ByNewPrefabResourceInstaller<SlotInstaller>("Prefabs/ItemSlot")
						.WithGameObjectName("ItemSlot"));
		}
	}
}