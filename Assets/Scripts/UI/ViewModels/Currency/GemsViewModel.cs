using System;
using MVVM;
using Sirenix.OdinInspector;
using UniRx;
using Zenject;

namespace SampleGame.UI
{
    public sealed class GemsViewModel : IInitializable, IDisposable
    {
        [Data("Currency")]
        [ShowInInspector, ReadOnly]
        public readonly ReactiveProperty<string> Gems = new();

        private readonly GemsStorage gemsStorage;

        public GemsViewModel(GemsStorage gemsStorage)
        {
            this.gemsStorage = gemsStorage;
        }

        public void Initialize()
        {
            this.OnGemsChanged(this.gemsStorage.Gems);
            this.gemsStorage.OnGemsChanged += this.OnGemsChanged;
        }

        public void Dispose()
        {
            this.gemsStorage.OnGemsChanged -= this.OnGemsChanged;
        }

        private void OnGemsChanged(short gems)
        {
            this.Gems.Value = "Gems: " + gems;
        }
    }
}