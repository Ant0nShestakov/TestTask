using UnityEngine;
using Zenject;

[RequireComponent(typeof(InputManager), typeof(UnitController), typeof(UnitView))]
public sealed class UnitMonoInsaller : MonoInstaller
{
    [SerializeField] private Transform _cameraTransform;

    [Header("Unit")]
    [SerializeField] private UnitStats _unitStats;
    [SerializeField] LayerMask _interactableLayers;

    private void BindHandlers()
    {
        var unit = GetComponent<UnitController>();

        Container.Bind<Handler>().To<MovementHandler>().AsCached().WithArguments(unit);
        Container.Bind<Handler>().To<AimHandler>().AsCached().WithArguments(_cameraTransform, unit);
        Container.Bind<Handler>().To<InteractionHandler>().AsCached().WithArguments(_interactableLayers);
    }
    private void BindUnitModel()
    {
        Container.BindInstance<UnitStats>(_unitStats);
        Container.Bind<AbstractUnitModel>().To<UnitModel>().AsSingle().NonLazy();
    }
   
    private void BindFSM()
    {
        Container.Bind<IActionStateVisitor>().To<MovementFSMVisitor>().AsCached();
        Container.Bind<IAnimationFSM>().To<MovementFSM>().AsCached().WithArguments(GetComponent<UnitView>());
    }

    public override void InstallBindings()
    {
        Container.Bind<InputManager>().FromInstance(GetComponent<InputManager>()).AsSingle().NonLazy();

        BindUnitModel();
        BindHandlers();
        BindFSM();
    }
}