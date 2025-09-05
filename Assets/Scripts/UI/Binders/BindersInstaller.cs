using MVVM;
using Zenject;

namespace SampleGame.UI
{
    public sealed class BindersInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BinderFactory.RegisterBinder<TextBinder>();
            BinderFactory.RegisterBinder<ImageBinder>();
            BinderFactory.RegisterBinder<ButtonBinder>();
            BinderFactory.RegisterBinder<ViewSetterBinder<bool>>();
            BinderFactory.RegisterBinder<ReactiveCollectionBinder<ProductView, ProductViewModel>>();
        }
    }
}