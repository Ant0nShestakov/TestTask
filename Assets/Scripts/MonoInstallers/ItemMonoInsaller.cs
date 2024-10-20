using UnityEngine;
using Zenject;

[RequireComponent(typeof(Animator))]
public sealed class ItemMonoInsaller : MonoInstaller
{
    [SerializeField] private ItemStats _itemStats;

    private void BindFSM()
    {
        Container.Bind<IAnimationFSM>().To<ItemFSM>().AsSingle().WithArguments(GetComponent<Animator>());
    }

    public override void InstallBindings()
    {
        BindFSM();

        Container.BindInstance(_itemStats);
    }
}