using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(PhysicsController))]
public abstract class AbstractUnitController : MonoBehaviour
{
    protected AbstractUnitModel model;
    protected UnitView view;

    protected IEnumerable<Handler> handlers;
}