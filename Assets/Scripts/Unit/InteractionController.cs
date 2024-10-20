using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] private LayerMask _interactionMask;
    private Camera _camera;
    private Vector3 _screenCenter;


    private void Awake()
    {
       // _playerModel = GetComponent<PlayerModel>();
        _camera = Camera.main;
        _screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0); 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = _camera.ScreenPointToRay(_screenCenter);

            //if(Physics.Raycast(ray, out RaycastHit hit, 5f, _interactionMask))
            //{
            //    if (hit.collider.TryGetComponent<InventoryItem>(out InventoryItem item))
            //        _playerModel.Inventory.Add(item);
            //}
        }
    }
}