using Sirenix.OdinInspector;
using UnityEngine;

namespace SampleGame
{
    [CreateAssetMenu(
        fileName = "Product",
        menuName = "Lessons/New Product (Presentation Model)"
    )]
    public sealed class Product : ScriptableObject
    {
        [PreviewField]
        public Sprite icon;

        public string title;

        [TextArea]
        public string description;
        
        [Space]
        public int price;
    }
}