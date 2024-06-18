using UnityEngine;

namespace SurvShoo
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class UpdateRotation : MonoBehaviour
    {
        [SerializeField]
        private float rotationSpeed;

        [SerializeField]
        private Vector3 rotationAxis = Vector3.up;

        private void Update()
        {
            transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
        }
    }
}
