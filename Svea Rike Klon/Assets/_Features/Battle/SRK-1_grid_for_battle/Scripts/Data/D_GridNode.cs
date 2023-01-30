using UnityEngine;
using System;

namespace SRK1_grid_for_battle
{
    [Serializable]
    public class D_GridNode : MonoBehaviour
    {

        public int x, z;
        public float sizeWidth, sizeDepth;
        //Soldier soldierOnSpace;

        public void Initialize(int p_x, int p_z, float p_sizeWidth, float p_sizeDepth)
        {
            x = p_x;
            z = p_z;
            sizeWidth = p_sizeWidth;
            sizeDepth = p_sizeDepth;
        }
    }
}
