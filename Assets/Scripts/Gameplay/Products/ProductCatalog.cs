using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SampleGame
{
    [CreateAssetMenu(
        fileName = "ProductCatalog",
        menuName = "Lessons/New ProductCatalog"
    )]
    public sealed class ProductCatalog : ScriptableObject, IEnumerable<Product>
    {
        public event Action<Product> OnProductAdded;
        public event Action<Product> OnProductRemoved; 

        [SerializeField]
        private List<Product> products;

        [Button]
        public void RemoveProduct(Product product)
        {
            this.products.Remove(product);
            this.OnProductRemoved?.Invoke(product);
        }
        
        [Button]
        public void AddProduct(Product product)
        {
            this.products.Add(product);
            this.OnProductAdded?.Invoke(product);
        }

        public IEnumerator<Product> GetEnumerator()
        {
            return this.products.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}