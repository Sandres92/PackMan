#if UNITY_EDITOR
using SampleGame.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace SampleGame
{
    public sealed class CurrencyDebug : MonoBehaviour
    {
        [Inject]
        [ShowInInspector, HideInEditorMode]
        private MoneyStorage moneyStorage;
        
        [Inject]
        [ShowInInspector, HideInEditorMode]
        private GemsStorage gemsStorage;

        [Inject]
        [ShowInInspector, HideInEditorMode]
        private MoneyViewModel moneyViewModel;
    }
}
#endif