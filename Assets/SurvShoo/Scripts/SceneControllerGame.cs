using UnityEngine;

namespace SurvShoo
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class SceneControllerGame : MonoBehaviour
    {
        async void Start()
        {
            await BootSystem.IsReady;
            Debug.Log("Game Start");
        }
    }
}
