using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SellManagerInstaller : MonoInstaller
{
    [SerializeField] private SellManager sellManager;

    public override void InstallBindings() =>
        Container.Bind<SellManager>().FromInstance(sellManager).AsSingle();
}
