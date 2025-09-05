using Sirenix.OdinInspector;
using UnityEngine;

namespace SampleGame
{
    public sealed class ProductBuyer
    {
        private readonly MoneyStorage moneyStorage;

        public ProductBuyer(MoneyStorage moneyStorage)
        {
            this.moneyStorage = moneyStorage;
        }
        
        [Button]
        public bool CanBuy(Product product)
        {
            return this.moneyStorage.Money >= product.price;
        }

        [Button]
        public void Buy(Product product)
        {
            if (this.CanBuy(product))
            {
                this.moneyStorage.SpendMoney(product.price);
                Debug.Log($"<color=green>Product {product.title} successfully purchased!</color>");
            }
            else
            {
                Debug.LogWarning($"<color=red>Not enough money for product {product.title}!</color>");
            }
        }
    }
}