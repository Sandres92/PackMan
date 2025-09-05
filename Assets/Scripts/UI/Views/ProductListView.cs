using MVVM;
using UnityEngine;

namespace SampleGame.UI
{
    public sealed class ProductListView : MonoBehaviour
    {
        [Data("Items")]
        public CollectionView<ProductView> collection;
    }
}