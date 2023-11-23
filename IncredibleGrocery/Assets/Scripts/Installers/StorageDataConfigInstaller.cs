using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StorageDataConfigInstaller : MonoInstaller
{
    [SerializeField] private StorageDataConfig storageDataConfig;

    public override void InstallBindings() =>
        Container.Bind<StorageDataConfig>().FromInstance(storageDataConfig).AsSingle();
}
