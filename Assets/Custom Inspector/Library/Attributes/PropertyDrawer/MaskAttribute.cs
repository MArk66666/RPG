using System;
using System.Diagnostics;
using UnityEngine;


namespace CustomInspector
{
    [AttributeUsage(AttributeTargets.Field)]
    [Conditional("UNITY_EDITOR")]
    public class MaskAttribute : PropertyAttribute
    {
        public readonly int bitsAmount = 3;
        public MaskAttribute() { }
        /// <summary>
        /// bitsAmount is only used for integers and not enums
        /// </summary>
        /// <param name="bitsAmount"></param>
        public MaskAttribute(int bitsAmount)
        {
            this.bitsAmount = bitsAmount;
        }
    }
}