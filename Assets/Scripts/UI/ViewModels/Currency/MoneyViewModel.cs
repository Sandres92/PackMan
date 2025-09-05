using System;
using MVVM;
using Sirenix.OdinInspector;
using UniRx;
using Zenject;

namespace SampleGame.UI
{
    public sealed class MoneyViewModel : IInitializable, IDisposable
    {
        [Data("Currency")]
        [ShowInInspector, ReadOnly]
        public readonly ReactiveProperty<string> Money = new();

        private readonly MoneyStorage moneyStorage;

        public MoneyViewModel(MoneyStorage moneyStorage)
        {
            this.moneyStorage = moneyStorage;
        }

        public void Initialize()
        {
            this.OnMoneyChanged(this.moneyStorage.Money);
            this.moneyStorage.OnStateChanged += this.OnMoneyChanged;
        }

        public void Dispose()
        {
            this.moneyStorage.OnStateChanged -= this.OnMoneyChanged;
        }

        private void OnMoneyChanged(int money)
        {
            this.Money.Value = money + "$";
        }
    }
}