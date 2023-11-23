using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ProductItemViewFactoryInstaller : MonoInstaller
{
    [SerializeField] private ProductItemViewFactory productItemViewFactory;
    public override void InstallBindings() =>
        Container.Bind<ProductItemViewFactory>().FromInstance(productItemViewFactory).AsSingle();
}
