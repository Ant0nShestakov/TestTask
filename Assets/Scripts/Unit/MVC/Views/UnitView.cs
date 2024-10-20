using ModestTree;
using UnityEngine;
using Zenject;

[RequireComponent (typeof(Animator), typeof(UnitController))]
public sealed class UnitView : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private string _hMoveName;
    [SerializeField] private string _vMoveName;

    [SerializeField] private float _dampTime;

    private Animator _animator;

    private IAnimationFSM[] _fsms;
    
    public Animator Animator => _animator;

    [Inject]
    public void Construct(IAnimationFSM[] fsms)
    {
        _fsms = fsms;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        foreach (var fsm in _fsms) 
            fsm.Update();
    }

    private void OnValidate()
    {
        if (_hMoveName.IsEmpty())
            throw new System.ArgumentNullException();

        if (_vMoveName.IsEmpty())
            throw new System.ArgumentNullException();
    }

    public void AnimateMovement(in Vector2 moveVector)
    {
        _animator.SetFloat(_hMoveName, moveVector.x, _dampTime, Time.deltaTime);
        _animator.SetFloat(_vMoveName, moveVector.y, _dampTime, Time.deltaTime);
    }
}