using UnityEngine;
using Zenject;

public class BuyerSpawnerConfigInstaller : MonoInstaller
{
    [SerializeField] private BuyerSpawnerConfig buyerSpawnerConfig;

    public override void InstallBindings() =>
        Container.Bind<BuyerSpawnerConfig>().FromInstance(buyerSpawnerConfig).AsSingle();
}
