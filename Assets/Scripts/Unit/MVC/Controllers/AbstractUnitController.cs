using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent (typeof(PhysicsController))]
public abstract class AbstractUnitController : MonoBehaviour
{
    protected AbstractUnitModel model;
    protected UnitView view;

    protected IEnumerable<Handler> handlers;
}