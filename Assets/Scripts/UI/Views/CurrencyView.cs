using MVVM;
using TMPro;
using UnityEngine;

namespace SampleGame.UI
{
    public class CurrencyView : MonoBehaviour
    {
        [Data("Currency")]
        public TMP_Text currencyText;
    }
}