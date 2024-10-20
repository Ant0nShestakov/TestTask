using UnityEngine;
using Zenject;

public enum Primitive
{
    Ray,
    Sphere
}

[RequireComponent(typeof(Rigidbody))]
public class PhysicsController : MonoBehaviour, IMoveble
{
    [Header("Collision settings")]
    [SerializeField] private Primitive _primitive;
    [SerializeField] private float _collisionDistance;
    [SerializeField] private LayerMask _collisionMask;

    [Header("Move settings")]
    [SerializeField] private float _gravity;

    [SerializeField] private float _offsetDistance;
    [SerializeField] private float _stepOffset;
    [SerializeField] private float _slopeLimit;
    [SerializeField] private float _climbForce;

    private PhysicsSettings _physicsSettings;

    private Rigidbody _rigidbody;

    private Vector3 _moveDirection;
    private Vector3 _climbVector;
    private Vector3 _offset;
    private Vector3 _from;

    private Vector3 _projectedDirection;

    private bool _isSlopeLimit;

    float _slopeAngle;

    public bool IsGrounded { get; private set; }
    public bool IsMoved { get; private set; }

    [Inject]
    public void Construct(PhysicsSettings settings)
    {
        _physicsSettings = settings;
    }

    private void Awake()
    {
        _climbVector = new Vector3(0, _climbForce, 0);
        _offset = new Vector3(0, _offsetDistance, 0);
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_primitive == Primitive.Sphere)
            IsGrounded = Physics.CheckSphere(transform.position, _collisionDistance, _collisionMask);
        else
        {
            _from = transform.TransformDirection(Vector3.down);
            IsGrounded = Physics.Raycast(transform.position, _from, _collisionDistance, _collisionMask);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_primitive == Primitive.Sphere)
            Gizmos.DrawSphere(transform.position, _collisionDistance);
        else
            Gizmos.DrawLine(transform.position, transform.position + _from * _collisionDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, _moveDirection);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + new Vector3(0, _stepOffset, 0), _moveDirection + new Vector3(0, _stepOffset, 0));
    }

    private bool StepOffset()
    {
        if (!Physics.Raycast(transform.position, _moveDirection, _offsetDistance))
            return true;

        if (_slopeAngle > _slopeLimit && _slopeAngle != 90)
            return false;

        Vector3 root = transform.position + _offset;
        Vector3 move = _moveDirection + _offset;

        if (!Physics.Raycast(root, move, _offsetDistance))
        {
            if(_slopeAngle > _slopeLimit && _slopeAngle != 90)
                return false;

           _rigidbody.position += _climbVector * Time.deltaTime;
            return true;
        }

        return false;
    }

    private void SlopeLimit()
    {
        Vector3 downDirection = transform.TransformDirection(Vector3.down);

        if (!Physics.Raycast(transform.position, downDirection, out RaycastHit hit, Mathf.Infinity))
            return;

        if (!IsGrounded)
            return;

        _slopeAngle = Vector3.Angle(hit.normal, transform.TransformDirection(Vector3.up));

        if (_slopeAngle == 0)
        {
            StepOffset();
            _projectedDirection = _moveDirection;
            _isSlopeLimit = false;
            return;
        }

        _projectedDirection = ProjectOnSlope(_moveDirection, hit);

        if (_slopeAngle < _slopeLimit)
        {
            _isSlopeLimit = false;
            return;
        }

        float dot = _projectedDirection.normalized.y;

        if (dot > 0 && !StepOffset())
        {
            _projectedDirection = Vector3.zero;
            _isSlopeLimit = true;

            return;
        }

        if (_projectedDirection == Vector3.zero)
        {
            _projectedDirection = ProjectOnSlope(Vector3.down, hit);
        }

        _projectedDirection *= _climbForce;
        _isSlopeLimit = false;

    }

    private Vector3 ProjectOnSlope(in Vector3 moveDirection, in RaycastHit slopeHit) =>
        Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);

    private void CalculateDirectionPerMass(float mass) =>
        _physicsSettings.CalculateDirection(ref _projectedDirection, mass);

    public void Move(in Vector3 moveDirection, float mass)
    {
        if (moveDirection == Vector3.zero)
        {
            IsMoved = false;
            return;
        }

        _moveDirection = moveDirection;

        SlopeLimit();

        CalculateDirectionPerMass(mass);

        if (!IsGrounded)
            _projectedDirection.y -= _gravity * Time.deltaTime;

        _rigidbody.MovePosition(_rigidbody.position + _projectedDirection * Time.deltaTime);
        IsMoved = true;
    }

    public void Jump(float jumpForce)
    {
        if (!_isSlopeLimit && IsGrounded)
        {
            Vector3 velocity = _rigidbody.velocity;
            velocity.y = jumpForce;
            _rigidbody.velocity = velocity;
        }
    }
}
