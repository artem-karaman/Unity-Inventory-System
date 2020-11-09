using System;
using UnityEngine;
using UnityInventorySystem.Installers;
using Zenject;

[CreateAssetMenu(fileName = "MainInventorySOInstaller", menuName = "SO/Installers/MainInventorySOInstaller")]
public class MainInventorySOInstaller : ScriptableObjectInstaller<MainInventorySOInstaller>
{
    [SerializeField] private MainInventoryMonoInstaller.Settings _mainInventoryMonoInstallerSettings;
    public override void InstallBindings()
    {
        Container.BindInstance(_mainInventoryMonoInstallerSettings).IfNotBound();
        
    }
}