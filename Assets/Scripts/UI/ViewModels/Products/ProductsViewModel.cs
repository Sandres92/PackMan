using System;
using System.Collections;
using System.Collections.Generic;
using MVVM;
using Sirenix.OdinInspector;
using UniRx;
using Zenject;

namespace SampleGame
{
    [Serializable]
    public sealed class ProductsViewModel : IInitializable, IDisposable
    {
        [Data("Items")]
        [ShowInInspector, ReadOnly]
        public readonly ReactiveCollection<ProductViewModel> collection = new();
        
        private readonly ProductCatalog catalog;
        private readonly ProductViewModel.Factory itemFactory;
        private readonly Dictionary<Product, ProductViewModel> products = new();

        public ProductsViewModel(ProductCatalog catalog, ProductViewModel.Factory itemFactory)
        {
            this.catalog = catalog;
            this.itemFactory = itemFactory;
        }

        public void Initialize()
        {
            this.catalog.OnProductAdded += this.OnProductAdded;
            this.catalog.OnProductRemoved += this.OnProductRemoved;
            
            foreach (Product product in this.catalog)
            {
                ProductViewModel productVM = this.itemFactory.Create(product);
                productVM.Initialize();
                this.products.Add(product, productVM);
                this.collection.Add(productVM);
            }
        }

        public void Dispose()
        {
            this.catalog.OnProductAdded -= this.OnProductAdded;
            this.catalog.OnProductRemoved -= this.OnProductRemoved;

            foreach (ProductViewModel productVM in this.products.Values)
            {
                productVM.Dispose();
                this.collection.Remove(productVM);
            }

            this.products.Clear();
        }

        private void OnProductRemoved(Product product)
        {
            if (this.products.Remove(product, out ProductViewModel productVM))
            {
                productVM.Dispose();
                this.collection.Remove(productVM);
            }
        }

        private void OnProductAdded(Product product)
        {
            ProductViewModel productVM = this.itemFactory.Create(product);
            productVM.Initialize();
            this.products.Add(product, productVM);
            this.collection.Add(productVM);
        }

        public IEnumerator GetEnumerator()
        {
            return this.products.Values.GetEnumerator();
        }
    }
}