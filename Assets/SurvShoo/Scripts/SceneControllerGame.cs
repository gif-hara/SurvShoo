using UnityEngine;

namespace SurvShoo
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class SceneControllerGame : MonoBehaviour
    {
        [SerializeField]
        private GameDesign gameDesign;
        
        [SerializeField]
        private GameInstanceData gameInstanceData;
        
        [SerializeField]
        private Actor playerActorPrefab;
        
        async void Start()
        {
            await BootSystem.IsReady;
            TinyServiceLocator.Resolve<InputController>().InputActions.Enable();
            TinyServiceLocator.Register(gameInstanceData);
            TinyServiceLocator.Register(gameDesign);
            var playerActor = Instantiate(playerActorPrefab);
            var gamePlayerController = new GamePlayerController();
            gamePlayerController.Setup(playerActor);
        }
    }
}
