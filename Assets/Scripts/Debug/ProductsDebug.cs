#if UNITY_EDITOR
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace SampleGame
{
    public sealed class ProductsDebug : MonoBehaviour
    {
        [Inject]
        [ShowInInspector, HideInEditorMode]
        private ProductBuyer productBuyer;

        [Inject]
        [ShowInInspector, HideInEditorMode]
        private ProductViewModel productViewModel;
        
        [Inject]
        [ShowInInspector, HideInEditorMode]
        private ProductsViewModel productsViewModel;
    }
}
#endif