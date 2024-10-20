using System;
using UnityEngine;
using UnityEngine.InputSystem;

public sealed class InputManager : MonoBehaviour
{
    private BaseMap _inputMap;

    [field: SerializeField] public float Sensetivity { get; private set; }

    [field: SerializeField, Range(0, 90)] public float MaxClampAngle { get; private set; }

    [field: SerializeField, Range(-90, 0)] public float MinClampAngle { get; private set; }

    public Vector2 MoveVector { get; private set; }

    public Vector2 MouseVector { get; private set; }

    public float JumpValue {  get; private set; }

    public float InteractionValue { get; private set; }

    public float RunValue { get; private set; }

    private void Awake()
    {
        _inputMap = new BaseMap();

        SubscribePerformed();
        SubscribeCanceled();
    }

    private void OnEnable()
    {
        _inputMap.Enable();
    }

    private void Jump(InputAction.CallbackContext context) => 
        JumpValue = context.action.ReadValue<float>();

    private void Look(InputAction.CallbackContext context) => 
        MouseVector = context.action.ReadValue<Vector2>() * Sensetivity * Time.deltaTime;

    private void Move(InputAction.CallbackContext context) =>
        MoveVector = context.action.ReadValue<Vector2>();

    private void Run(InputAction.CallbackContext context) 
        => RunValue = context.action.ReadValue<float>();

    private void Interact(InputAction.CallbackContext context)
    => InteractionValue = context.action.ReadValue<float>();

    private void SubscribePerformed()
    {
        _inputMap.Player.Move.performed += Move;
        _inputMap.Player.Look.performed += Look;
        _inputMap.Player.Run.performed += Run;
        _inputMap.Player.Jump.performed += Jump;
        _inputMap.Player.Interact.performed += Interact;
    }

    private void SubscribeCanceled()
    {
        _inputMap.Player.Move.canceled += Move;
        _inputMap.Player.Look.canceled += Look;

        _inputMap.Player.Interact.canceled += Interact;
    }

    private void OnDisable()
    {
        _inputMap.Disable();
    }
}
