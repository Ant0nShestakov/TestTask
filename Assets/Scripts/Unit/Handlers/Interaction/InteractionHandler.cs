using UnityEngine;

public sealed class InteractionHandler : Handler
{
    private readonly AbstractUnitModel _unitModel;

    private readonly LayerMask _interactionLayers;
    private readonly float _interactionDistance;
    private readonly Transform _handTransform;

    private readonly Camera _camera;
    private Vector3 _screenCenter;

    private Item _currentItem;

    public InteractionHandler(InputManager inputManager, LayerMask interactionLayers, 
        float interactionDistance, Transform handTransform, AbstractUnitModel unitModel) : base(inputManager)
    {
        _interactionLayers = interactionLayers;
        _interactionDistance = interactionDistance;
        _handTransform = handTransform;

        _camera = Camera.main;

        _unitModel = unitModel;
    }

    private void SetItem(Item item)
    {
        _currentItem = item;
        _unitModel.AddMass(_currentItem.ItemStats.Mass);
        _currentItem.SetHand(_handTransform);
    }

    private void DropItem()
    {
        _unitModel.RemoveMass(_currentItem.ItemStats.Mass);
        _currentItem.Drop();
        _currentItem = null;
    }

    public override void Update()
    {
        if(Input.GetKeyUp(KeyCode.E))
        {
            _screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            Ray ray = _camera.ScreenPointToRay(_screenCenter);

            if (!Physics.Raycast(ray, out RaycastHit hit, _interactionDistance, _interactionLayers))
                return;

            if (!hit.collider.TryGetComponent<Item>(out Item item))
                return;

            if (_currentItem == null)
                SetItem(item);
            else
                DropItem();
        }
    }
}