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
			Container
				.Bind<Transform>()
				.FromComponentOnRoot();

			Container
				.BindInterfacesAndSelfTo<InventoryFacade>()
				.AsSingle();

			Container
				.BindFactory<ItemBehaviour, ItemBehaviour.Factory>()
				.FromNewComponentOnNewPrefabResource("Prefabs/Item")
				.WithGameObjectName("Item");
			
			Container
				.BindFactory<SlotBehaviour, SlotBehaviour.Factory>()
				.FromNewComponentOnNewPrefabResource("Prefabs/ItemSlot")
				.WithGameObjectName("ItemSlot");

			Container
				.Bind<ItemPoolBehaviour>()
				.ToSelf()
				.AsSingle();

			Container
				.Bind<SlotsPoolBehaviour>()
				.ToSelf()
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