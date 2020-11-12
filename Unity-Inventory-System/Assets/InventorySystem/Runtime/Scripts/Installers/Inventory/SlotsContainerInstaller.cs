using UnityInventorySystem.Inventory;
using Zenject;

namespace UnityInventorySystem.Installers
{
	public class SlotsContainerInstaller : Installer<SlotsContainerInstaller>
	{
		public override void InstallBindings()
		{
			Container
				.Bind<SlotFacadesPoolBehaviour>()
				.ToSelf()
				.AsSingle();

			Container
				.BindFactory<SlotFacade, SlotFacade.Factory>()
				.FromMonoPoolableMemoryPool(x =>
					x
						.WithInitialSize(25)
						.FromSubContainerResolve()
						.ByNewPrefabResourceInstaller<SlotInstaller>("Prefabs/Inventories/ItemSlot")
						.WithGameObjectName("ItemSlot"));
		}
	}
}