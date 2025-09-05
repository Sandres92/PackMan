using UnityEngine;
using Zenject;

namespace SampleGame
{
    public sealed class ProductInstaller : MonoInstaller
    {
        [SerializeField]
        private ProductCatalog catalog;
        
        public override void InstallBindings()
        {
            this.Container.Bind<ProductBuyer>().AsSingle();
            this.Container.Bind<ProductCatalog>().FromInstance(this.catalog).AsSingle();
        }
    }
}