using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mazlo.Components
{
    public class InputComponent : MonoBehaviour
    {
        [HideInInspector] public float moveX;
        [HideInInspector] public float moveY;
        [HideInInspector] public float lookX;
        [HideInInspector] public float lookY;
    }
}
