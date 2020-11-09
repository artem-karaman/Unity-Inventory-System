using Assets.Scripts.Core.ViewModels;
using UnityInventorySystem.Inventory;
using Zenject;

namespace UnityInventorySystem.Installers
{
	public class InventoryInstaller : Installer<InventoryInstaller>
	{
		public override void InstallBindings()
		{
			Container
				.BindFactory<SlotBehaviour, SlotBehaviour.Factory>()
				.FromNewComponentOnNewPrefabResource("Prefabs/ItemSlot")
				.WithGameObjectName("ItemSlot");
			
			Container
				.Bind<SlotsPoolBehaviour>()
				.ToSelf()
				.AsSingle();
			
			Container
				.BindInterfacesAndSelfTo<InventoryBehaviour>()
				.AsSingle();
			Container
				.BindInterfacesAndSelfTo<InventoryViewModel>()
				.AsSingle();
		}
	}
}