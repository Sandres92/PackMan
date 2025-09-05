using System;
using MVVM;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using Zenject;
// ReSharper disable RedundantNameQualifier

namespace SampleGame
{
    public sealed class ProductViewModel : IInitializable, IDisposable
    {
        [Data("Title")]
        [ShowInInspector, ReadOnly]
        public readonly ReactiveProperty<string> Title = new();

        [Data("Description")]
        [ShowInInspector, ReadOnly]
        public readonly ReactiveProperty<string> Description = new();

        [Data("Icon")]
        [ShowInInspector, ReadOnly]
        public readonly ReactiveProperty<Sprite> Icon = new();

        [Data("Price")]
        [ShowInInspector, ReadOnly]
        public readonly ReactiveProperty<string> Price = new();

        [Data("Interactible")]
        [ShowInInspector, ReadOnly]
        public readonly ReactiveProperty<bool> Interactible = new();

        private readonly ProductBuyer productBuyer;
        private readonly MoneyStorage moneyStorage;
        private Product product;

        public ProductViewModel(ProductBuyer productBuyer, MoneyStorage moneyStorage, [InjectOptional] Product product)
        {
            this.productBuyer = productBuyer;
            this.moneyStorage = moneyStorage;
            this.product = product;
        }

        public void Initialize()
        {
            this.moneyStorage.OnStateChanged += this.OnMoneyChanged;
            this.UpdateProduct();
        }

        public void Dispose()
        {
            this.moneyStorage.OnStateChanged -= this.OnMoneyChanged;
        }

        [Sirenix.OdinInspector.Button]
        public void SetProduct(Product product)
        {
            this.product = product;
            this.UpdateProduct();
        }

        [Method("OnBuyClick")]
        public void OnBuyClicked()
        {
            if (this.productBuyer.CanBuy(this.product))
            {
                this.productBuyer.Buy(this.product);
            }
        }

        private void OnMoneyChanged(int _)
        {
            if (this.product != null)
            {
                this.Interactible.Value = this.product != null && this.productBuyer.CanBuy(this.product);
            }
        }

        private void UpdateProduct()
        {
            this.Title.Value = this.product != null ? this.product.title : string.Empty;
            this.Description.Value = this.product != null ? this.product.description : string.Empty;
            this.Icon.Value = this.product != null ? this.product.icon : null;
            this.Price.Value = this.product != null ? this.product.price.ToString() : string.Empty;
            this.Interactible.Value = this.product != null && this.productBuyer.CanBuy(this.product);
        }

        public sealed class Factory : PlaceholderFactory<Product, ProductViewModel>
        {
        }
    }
}