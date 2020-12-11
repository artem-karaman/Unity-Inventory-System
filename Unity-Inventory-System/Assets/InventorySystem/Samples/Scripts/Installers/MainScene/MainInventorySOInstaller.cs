using System;
using UnityEngine;
using UnityInventorySystem.Installers;
using Zenject;

[CreateAssetMenu(fileName = "MainInventorySOInstaller", menuName = "SO/Installers/MainInventorySOInstaller")]
public class MainInventorySOInstaller : ScriptableObjectInstaller<MainInventorySOInstaller>
{
    #pragma warning disable 0649
    [SerializeField] private MainInventoryMonoInstaller.Settings _mainInventoryMonoInstallerSettings;
    #pragma warning restore 0649
    public override void InstallBindings()
    {
        Container.BindInstance(_mainInventoryMonoInstallerSettings).IfNotBound();
    }
}