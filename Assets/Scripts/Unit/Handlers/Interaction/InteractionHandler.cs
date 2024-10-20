using UnityEngine;

public sealed class InteractionHandler : Handler
{
    private LayerMask _interactableLayers;
    private Vector3 _screenCenter;
    private readonly Camera _camera;
    private readonly int _interactableDistance;

    public InteractionHandler(InputManager inputManager, LayerMask interactableLayers) : base(inputManager)
    {
        _interactableLayers = interactableLayers;
        _camera = Camera.main;

        _screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
    }

    public override void Update()
    {
        if(inputManager.InteractionValue > 0)
        {
            Ray ray = _camera.ScreenPointToRay(_screenCenter);

            if (!Physics.Raycast(ray, out RaycastHit hit, _interactableDistance, _interactableLayers))
                return;

            if (!hit.collider.TryGetComponent<UnitView>(out UnitView unitView))
                return;


        }
    }
}