using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent (typeof(Rigidbody))]
public class Item : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Collider _collider;

    private IAnimationFSM _fsm;

    public ItemStats ItemStats { get; private set; }

    [Inject]
    public void Construct(IAnimationFSM fsm, ItemStats itemStats)
    {
        _fsm = fsm;
        ItemStats = itemStats;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    public void SetHand(Transform transform)
    {
        _rigidbody.isKinematic = true;
        _collider.isTrigger = true;

        gameObject.transform.SetParent(transform);
        gameObject.transform.localPosition = new Vector3(0, 0, 0);

        _fsm.SwitchState(_fsm.States.GetValueOrDefault(typeof(PickedUp)));
    }

    public void Drop()
    {
        gameObject.transform.SetParent(null);

        _collider.isTrigger = false;
        _rigidbody.isKinematic = false;

        _fsm.SwitchState(_fsm.States.GetValueOrDefault(typeof(Dropped)));
    }
}
