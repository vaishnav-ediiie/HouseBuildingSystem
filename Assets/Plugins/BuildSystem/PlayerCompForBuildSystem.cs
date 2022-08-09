using System;
using UnityEngine;
using BuildSystemSpace;

namespace BuildSystemSpace
{
    public class PlayerCompForBuildSystem : MonoBehaviour
    {
        [SerializeField] private float groundFloorHeight = 0f;
        [SerializeField] private float singleFloorHeight = 2f;

        public int GetFloorNumber() => (int)((transform.position.y - groundFloorHeight) / singleFloorHeight);
        public float GetFloorHeight() => GetFloorNumber() * singleFloorHeight + groundFloorHeight;
        
    }
}