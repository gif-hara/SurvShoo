using R3;
using UnityEngine.InputSystem;

namespace SurvShoo
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class Extensions
    {
        public static Observable<InputAction.CallbackContext> PerformedAsObservable(this InputAction self)
        {
            return Observable.FromEvent<InputAction.CallbackContext>(
                h => self.performed += h,
                h => self.performed -= h
            );
        }
        
        public static Observable<InputAction.CallbackContext> CanceledAsObservable(this InputAction self)
        {
            return Observable.FromEvent<InputAction.CallbackContext>(
                h => self.canceled += h,
                h => self.canceled -= h
            );
        }
        
        public static Observable<InputAction.CallbackContext> StartedAsObservable(this InputAction self)
        {
            return Observable.FromEvent<InputAction.CallbackContext>(
                h => self.started += h,
                h => self.started -= h
            );
        }
    }
}
