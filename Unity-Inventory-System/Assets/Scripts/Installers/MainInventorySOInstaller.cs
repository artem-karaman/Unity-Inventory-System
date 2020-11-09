using Assets.Scripts.Core.ViewModels;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "MainInventorySOInstaller", menuName = "SO/Installers/MainInventorySOInstaller")]
public class MainInventorySOInstaller : ScriptableObjectInstaller<MainInventorySOInstaller>
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