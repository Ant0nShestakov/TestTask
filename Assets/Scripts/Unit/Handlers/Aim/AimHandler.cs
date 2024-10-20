using UnityEngine;

public sealed class AimHandler : Handler
{
    private readonly Transform _cameraTransform;
    private readonly Transform _rootTransform;

    private readonly Vector2 _clampAngle;

    private float _mouseX;
    private float _mouseY;

    public AimHandler(Transform cameraTransform, AbstractUnitController controller, InputManager inputManager) : base(inputManager)
    {
        Cursor.lockState = CursorLockMode.Locked;
        _cameraTransform = cameraTransform;

        _rootTransform = controller.transform;

        _clampAngle = new Vector2 (this.inputManager.MinClampAngle, this.inputManager.MaxClampAngle);

        _mouseX = _rootTransform.rotation.eulerAngles.y; 
        _mouseY = _cameraTransform.localRotation.eulerAngles.y;
    }

    public override void Update()
    {
        _mouseX += inputManager.MouseVector.x;
        _mouseY -= inputManager.MouseVector.y;

        _mouseY = Mathf.Clamp(_mouseY, _clampAngle.x, _clampAngle.y);

        _cameraTransform.localRotation = Quaternion.Euler(_mouseY, 0, 0);
        _rootTransform.rotation = Quaternion.Euler(0, _mouseX, 0);
    }
}
