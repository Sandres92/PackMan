using Zenject;

namespace SampleGame.UI
{
    public sealed class ViewModelsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            this.Container
                .BindInterfacesAndSelfTo<MoneyViewModel>()
                .AsSingle()
                .NonLazy();

            this.Container
                .BindInterfacesAndSelfTo<GemsViewModel>()
                .AsSingle()
                .NonLazy();

            this.Container
                .BindInterfacesAndSelfTo<ProductViewModel>()
                .AsSingle()
                .NonLazy();
            
            this.Container
                .BindFactory<Product, ProductViewModel, ProductViewModel.Factory>()
                .AsSingle()
                .NonLazy();
            
            this.Container
                .BindInterfacesAndSelfTo<ProductsViewModel>()
                .AsSingle()
                .NonLazy();
        }
    }
}