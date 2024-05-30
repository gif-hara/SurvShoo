using UnityEngine;

namespace SurvShoo
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class SceneControllerGame : MonoBehaviour
    {
        [SerializeField]
        private Actor playerActorPrefab;
        
        async void Start()
        {
            await BootSystem.IsReady;
            TinyServiceLocator.Resolve<InputController>().InputActions.Enable();
            var playerActor = Instantiate(playerActorPrefab);
            var gamePlayerController = new GamePlayerController();
            gamePlayerController.Setup(playerActor);
        }
    }
}
