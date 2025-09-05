using MVVM;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SampleGame.UI
{
    public sealed class ProductView : MonoBehaviour
    {
        [Data("Title")]
        public TMP_Text title;

        [Data("Description")]
        public TMP_Text description;

        [Data("Icon")]
        public Image icon;

        [Data("Price")]
        public TMP_Text price;

        [Data("OnBuyClick")]
        public Button buyButton;

        [Setter("Interactible")]
        public bool Interactible
        {
            set { this.buyButton.interactable = value; }
        }

        [Sirenix.OdinInspector.Button]
        public void Show()
        {
            this.gameObject.SetActive(true);
        }

        [Sirenix.OdinInspector.Button]
        public void Hide()
        {
            this.gameObject.SetActive(false);
        }
    }
}