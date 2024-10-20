using UnityEngine;
using Zenject;

[RequireComponent(typeof(UnitView))]
public sealed class UnitController : AbstractUnitController
{
    private UnitView _view;

    [Inject]
    public void Construct(AbstractUnitModel model, Handler[] handlers)
    {
        this.model = model;
        this.handlers = handlers;
    }

    private void Awake()
    {
        _view = GetComponent<UnitView>();
    }

    private void Update()
    {
        foreach (var handler in handlers) 
            handler.Update();
    }

    public void SetWalkSpeed()
    {
        ((UnitModel)model).SetWalkSpeed();
    }
}