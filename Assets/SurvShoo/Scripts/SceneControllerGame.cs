using UnityEngine;

namespace SurvShoo
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class SceneControllerGame : MonoBehaviour
    {
        [SerializeField]
        private GameDesignData gameDesignData;

        [SerializeField]
        private GameInstanceData gameInstanceData;

        [SerializeField]
        private ActorSpawner debugEnemySpawner;

        [SerializeField]
        private Transform debugEnemySpawnerPoint;

        async void Start()
        {
            await BootSystem.IsReady;
            TinyServiceLocator.Resolve<InputController>().InputActions.Enable();
            TinyServiceLocator.Register(gameInstanceData);
            TinyServiceLocator.Register(gameDesignData);
            TinyServiceLocator.Register(new ActorPool());
            gameDesignData.PlayerData.ActorSpawner.Spawn(Vector3.zero, Quaternion.identity);

            if (debugEnemySpawner != null && debugEnemySpawnerPoint != null)
            {
                debugEnemySpawner.Spawn(debugEnemySpawnerPoint.position, debugEnemySpawnerPoint.rotation);
            }
        }
    }
}
