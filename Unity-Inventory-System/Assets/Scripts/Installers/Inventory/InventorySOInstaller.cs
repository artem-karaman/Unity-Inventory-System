using Assets.Scripts.Core.ViewModels;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "InventorySOInstaller", menuName = "SO/Installers/InventorySOInstaller")]
public class InventorySOInstaller : ScriptableObjectInstaller<InventorySOInstaller>
{
	[SerializeField] 
	private InventoryViewModel.Settings _inventoryViewModelSettings;
	
	public override void InstallBindings()
	{
		Container
			.BindInstance(_inventoryViewModelSettings)
			.AsSingle();
	}
}