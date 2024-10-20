using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameInstaller", menuName = "ScriptableObjects/Installers/GameInstaller")]
public class GameInstaller : ScriptableObjectInstaller<GameInstaller>
{
    [Header("Physics")] 
    [SerializeField] private PhysicsSettings _physicsSettings;

    private void BindPhysics()
    {
        Container.BindInstance<PhysicsSettings>(_physicsSettings);
    }

    public override void InstallBindings()
    {
        BindPhysics();
    }
}