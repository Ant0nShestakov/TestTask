using UnityEngine;
using Zenject;

[RequireComponent(typeof(InputManager), typeof(UnitController), typeof(UnitView))]
public sealed class UnitMonoInsaller : MonoInstaller
{
    [SerializeField] private Transform _cameraTransform;

    [Header("Unit")]
    [SerializeField] private UnitStats _unitStats;

    [Header("Interaction Settings")]
    [SerializeField, Min(0.1f)] private float _interactionDistance;
    [SerializeField] private LayerMask _interactionLayers;
    [SerializeField] private Transform _handTransform;

    private void BindHandlers()
    {
        var unit = GetComponent<UnitController>();

        Container.Bind<Handler>().To<MovementHandler>().AsCached().WithArguments(unit);
        Container.Bind<Handler>().To<AimHandler>().AsCached().WithArguments(_cameraTransform, unit);
        Container.Bind<Handler>().To<InteractionHandler>().AsCached().WithArguments(_interactionLayers, _interactionDistance, _handTransform);
    }
    private void BindUnitModel()
    {
        Container.BindInstance<UnitStats>(_unitStats);
        Container.Bind<AbstractUnitModel>().To<UnitModel>().AsSingle().NonLazy();
    }
   
    private void BindFSM()
    {
        Container.Bind<IMovementActionStateVisitor>().To<MovementFSMVisitor>().AsCached();
        Container.Bind<IUpdatedAnimationFSM>().To<MovementFSM>().AsCached().WithArguments(GetComponent<UnitView>());
    }

    public override void InstallBindings()
    {
        Container.Bind<InputManager>().FromInstance(GetComponent<InputManager>()).AsSingle().NonLazy();

        BindUnitModel();
        BindHandlers();
        BindFSM();
    }
}