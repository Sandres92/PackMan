using System;
using Sirenix.OdinInspector;

namespace SampleGame
{
    public sealed class MoneyStorage
    {
        public event Action<int> OnStateChanged;

        [ShowInInspector, ReadOnly]
        public int Money { get; private set; }

        [Button]
        public void SetMoney(int money)
        {
            this.Money = money;
            this.OnStateChanged?.Invoke(this.Money);
        }

        [Button]
        public void AddMoney(int range)
        {
            this.Money += range;
            this.OnStateChanged?.Invoke(this.Money);
        }

        [Button]
        public void SpendMoney(int range)
        {
            this.Money -= range;
            this.OnStateChanged?.Invoke(this.Money);
        }
    }
}