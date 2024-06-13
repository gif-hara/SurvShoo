using System.Threading;
using R3;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SurvShoo
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class DebugControllerGame
    {
        public static void Begin(CancellationToken scope)
        {
            var gameInstanceData = TinyServiceLocator.Resolve<GameInstanceData>();
            Observable.EveryUpdate(scope)
                .Subscribe(_ =>
                {
                    if (Keyboard.current.digit1Key.wasPressedThisFrame)
                    {
                        gameInstanceData.AddPlayerMoveSpeedLevel(1);
                        Debug.Log($"PlayerMoveSpeedLevel: {gameInstanceData.PlayerMoveSpeedLevel.Data}");
                    }
                    if (Keyboard.current.digit2Key.wasPressedThisFrame)
                    {
                        gameInstanceData.AddPlayerMoveSpeedLevel(-1);
                        Debug.Log($"PlayerMoveSpeedLevel: {gameInstanceData.PlayerMoveSpeedLevel.Data}");
                    }
                    if (Keyboard.current.digit3Key.wasPressedThisFrame)
                    {
                        gameInstanceData.AddPlayerFireCooldownLevel(1);
                        Debug.Log($"PlayerFireCooldownLevel: {gameInstanceData.PlayerFireCooldownLevel.Data}");
                    }
                    if (Keyboard.current.digit4Key.wasPressedThisFrame)
                    {
                        gameInstanceData.AddPlayerFireCooldownLevel(-1);
                        Debug.Log($"PlayerFireCooldownLevel: {gameInstanceData.PlayerFireCooldownLevel.Data}");
                    }
                    if (Keyboard.current.digit5Key.wasPressedThisFrame)
                    {
                        gameInstanceData.AddPlayerBulletFirePointLevel(1);
                        Debug.Log($"PlayerBulletFirePointLevel: {gameInstanceData.PlayerBulletFirePointLevel.Data}");
                    }
                    if (Keyboard.current.digit6Key.wasPressedThisFrame)
                    {
                        gameInstanceData.AddPlayerBulletFirePointLevel(-1);
                        Debug.Log($"PlayerBulletFirePointLevel: {gameInstanceData.PlayerBulletFirePointLevel.Data}");
                    }
                })
                .RegisterTo(scope);
        }
    }
}
