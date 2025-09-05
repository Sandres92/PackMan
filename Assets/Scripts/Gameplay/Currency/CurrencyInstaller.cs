using UnityEngine;
using Zenject;

namespace SampleGame
{
    public sealed class CurrencyInstaller : MonoInstaller
    {
        [SerializeField]
        private int initialMoney = 1000;

        [SerializeField]
        private short initialGems = 50;

        public override void InstallBindings()
        {
            this.Container
                .Bind<MoneyStorage>()
                .AsSingle()
                .OnInstantiated<MoneyStorage>((_, it) => it.SetMoney(this.initialMoney))
                .NonLazy();
            
            this.Container
                .Bind<GemsStorage>()
                .AsSingle()
                .OnInstantiated<GemsStorage>((_, it) => it.SetGems(this.initialGems))
                .NonLazy();
        }
    }
}