using System;
using UnityEngine;

namespace SurvShoo
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public struct MinMaxValue
    {
        public float Min;
        public float Max;

        public MinMaxValue(float min, float max)
        {
            Min = min;
            Max = max;
        }

        public float Evaluate(float value)
        {
            return Mathf.Lerp(Min, Max, value);
        }
    }
}
