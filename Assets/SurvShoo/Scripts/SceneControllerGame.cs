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

        async void Start()
        {
            await BootSystem.IsReady;
            TinyServiceLocator.Resolve<InputController>().InputActions.Enable();
            TinyServiceLocator.Register(gameInstanceData);
            TinyServiceLocator.Register(gameDesignData);
            TinyServiceLocator.Register(new ActorPool());
            gameDesignData.PlayerData.ActorSpawner.Spawn(Vector3.zero, Quaternion.identity);
        }
    }
}
