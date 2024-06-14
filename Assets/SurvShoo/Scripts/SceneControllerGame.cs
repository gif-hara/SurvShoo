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
        private Transform playerSpawnPoint;

        [SerializeField]
        private ActorSpawner debugEnemySpawner;

        [SerializeField]
        private Transform debugEnemySpawnerPoint;

        async void Start()
        {
            Application.targetFrameRate = 60;
            await BootSystem.IsReady;
            TinyServiceLocator.Resolve<InputController>().InputActions.Enable();
            TinyServiceLocator.Register(gameInstanceData);
            TinyServiceLocator.Register(gameDesignData);
            TinyServiceLocator.Register(new ActorPool());
            TinyServiceLocator.Register(new ActorManager());
#if DEBUG
            DebugControllerGame.Begin(this.destroyCancellationToken);
#endif
            gameDesignData.PlayerData.ActorSpawner.Spawn(playerSpawnPoint.position, playerSpawnPoint.rotation);

            if (debugEnemySpawner != null && debugEnemySpawnerPoint != null)
            {
                debugEnemySpawner.Spawn(debugEnemySpawnerPoint.position, debugEnemySpawnerPoint.rotation);
            }
        }
    }
}
