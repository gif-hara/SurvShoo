using System.Threading;
using R3;
using Unity.VisualScripting;
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
                    if (Keyboard.current.digit7Key.wasPressedThisFrame)
                    {
                        AddOptionLevel(0, 1);
                    }
                    if (Keyboard.current.digit8Key.wasPressedThisFrame)
                    {
                        AddOptionLevel(1, 1);
                    }
                    if (Keyboard.current.digit9Key.wasPressedThisFrame)
                    {
                        AddOptionLevel(2, 1);
                    }
                    if (Keyboard.current.digit0Key.wasPressedThisFrame)
                    {
                        AddOptionLevel(3, 1);
                    }
                    if (Keyboard.current.qKey.wasPressedThisFrame)
                    {
                        AddOptionLevel(4, 1);
                    }
                    if (Keyboard.current.wKey.wasPressedThisFrame)
                    {
                        AddOptionLevel(5, 1);
                    }
                    if (Keyboard.current.eKey.wasPressedThisFrame)
                    {
                        AddOptionLevel(6, 1);
                    }
                })
                .RegisterTo(scope);

            void AddOptionLevel(int index, int value)
            {
                gameInstanceData.AddOptionLevel(index, value);
                Debug.Log($"OptionLevel{index}: {gameInstanceData.OptionLevels[index].Data}");
            }
        }
    }
}
