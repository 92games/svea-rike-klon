using System.Collections.Generic;
using System;
using UnityEngine;

namespace SRK1_grid_for_battle
{
    [Serializable]
    public class D_Grid
    {
        public List<D_GridNode> gridNodes;
        public int depth, width;
        public float nodeSizeWidth, nodeSizeDepth;
        public GameObject nodeObject;
    }
}
