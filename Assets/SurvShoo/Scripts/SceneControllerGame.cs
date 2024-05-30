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
            var playerActor = Instantiate(gameDesignData.PlayerData.ActorPrefab);
            var gamePlayerController = new GamePlayerController();
            gamePlayerController.Setup(playerActor);
        }
    }
}
