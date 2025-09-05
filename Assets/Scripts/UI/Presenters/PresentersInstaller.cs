using UnityEngine;
using Zenject;

namespace SampleGame
{
    public class PresentersInstaller : MonoInstaller
    {
        [SerializeField]
        private CurrencyViewAnimated gemsView;

        public override void InstallBindings()
        {
            this.Container
                .BindInterfacesTo<GemsPresenterAnimated>()
                .AsSingle()
                .WithArguments(this.gemsView)
                .NonLazy();
        }
    }
}