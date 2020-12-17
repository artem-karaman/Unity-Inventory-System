﻿using InventorySystem.Runtime.Scripts.Core.ViewModels.Inventory;
using InventorySystem.Runtime.Scripts.Inventory.Slot;
using Zenject;

namespace InventorySystem.Runtime.Scripts.Installers.Inventory
{
	public class SlotInstaller : Installer<SlotInstaller>
	{
		public override void InstallBindings()
		{
			Container
				.BindInterfacesAndSelfTo<SlotViewModel>()
				.AsSingle();
			
			Container
				.BindInterfacesAndSelfTo<SlotView>()
				.AsSingle();

			Container
				.BindInterfacesAndSelfTo<SlotBehaviour>()
				.AsSingle();
			
			Container
				.BindInterfacesAndSelfTo<SlotFacade>()
				.FromNewComponentOnRoot()
				.AsSingle();
		}
	}
}