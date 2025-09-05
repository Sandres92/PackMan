using System;
using Zenject;

namespace SampleGame
{
    public sealed class GemsPresenterAnimated : IInitializable, IDisposable
    {
        private readonly GemsStorage gemsStorage;
        private readonly CurrencyViewAnimated currencyView;
        
        private short _currentGems;

        public GemsPresenterAnimated(GemsStorage gemsStorage, CurrencyViewAnimated currencyView)
        {
            this.gemsStorage = gemsStorage;
            this.currencyView = currencyView;
        }

        public void Initialize()
        {
            _currentGems = this.gemsStorage.Gems;

            this.gemsStorage.OnGemsChanged += this.OnGemsChanged;
            this.currencyView.SetCurrency(_currentGems.ToString());
        }

        public void Dispose()
        {
            this.gemsStorage.OnGemsChanged -= this.OnGemsChanged;
        }

        private void OnGemsChanged(short gems)
        {
            int diff = gems - _currentGems;
            
            if (diff > 0) //Add
            {
                this.currencyView.EarnCurrency(_currentGems, diff, "{0}");
            }
            else if (diff < 0) //Remove
            {
                this.currencyView.SpendCurrency(gems.ToString());
            }

            _currentGems = gems;
        }
    }
}