using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class OrderManagerInstaller : MonoInstaller
{
    [SerializeField] private OrderManager orderManager;

    public override void InstallBindings() =>
        Container.Bind<OrderManager>().FromInstance(orderManager).AsSingle();
}
