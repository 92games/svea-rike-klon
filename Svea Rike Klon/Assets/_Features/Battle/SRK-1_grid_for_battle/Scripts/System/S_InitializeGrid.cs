using System;
using UnityEngine;

namespace SRK1_grid_for_battle
{
    [ExecuteAlways]
    public class S_InitializeGrid : MonoBehaviour
    {
        public float randomRange = 0.25f;
        public D_Grid grid;

        private int _gridDepth = 0;
        private int _gridWidth = 0;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        void Update()
        {
            UpdateableGrid();
        }

        void InstantiateAndAddNode(int p_x, int p_z)
        {
            grid.gridNodes ??= new();


            if(grid.gridNodes.Count < grid.depth * grid.width)
            {
                int nodeX = p_x;
                int nodeZ = p_z;
                Vector3 nodePosition = new Vector3(nodeX * grid.nodeSizeWidth + transform.position.x, transform.position.y + UnityEngine.Random.Range(-randomRange, randomRange), nodeZ * grid.nodeSizeDepth + transform.position.z);
                GameObject node = Instantiate(grid.nodeObject, Vector3.zero, Quaternion.identity);
                D_GridNode gridNode = node.AddComponent<D_GridNode>();
                gridNode.Initialize(nodeX, nodeZ, grid.nodeSizeWidth, grid.nodeSizeDepth);
                node.name = "Node " + nodeX + " " + nodeZ;
                node.transform.localScale = new Vector3(grid.nodeSizeWidth, transform.localScale.y, grid.nodeSizeDepth);
                node.transform.parent = transform;
                node.transform.Translate(nodePosition);
                grid.gridNodes.Add(gridNode);
            }
        }

        void RemoveNode(int p_x, int p_z)
        {
            D_GridNode node = grid.gridNodes.Find(node => node.x == p_x && node.z == p_z);
            if(node != null)
            {
                grid.gridNodes.Remove(node);
                DestroyImmediate(node.gameObject);
            }
        }

        void AddDepth()
        {
            for(int z = _gridDepth; z < grid.depth; z++)
            {
                for(int x = 0; x < _gridWidth; x++)
                {
                    InstantiateAndAddNode(x, z);
                }
            }

            _gridDepth = grid.depth;
        }

        void RemoveDepth()
        {
            for(int z = _gridDepth; z >= grid.depth; z--)
            {
                for(int x = _gridWidth -1 ; x >= 0; x--)
                {
                    RemoveNode(x, z);
                }
            }

            _gridDepth = grid.depth;
        }

        void AddWidth()
        {
            for(int x = _gridWidth; x < grid.width; x++)
            {
                for(int z = 0; z < _gridDepth; z++)
                {
                    InstantiateAndAddNode(x, z);
                }
            }

            _gridWidth = grid.width;
        }

        void RemoveWidth()
        {
            for(int x = _gridWidth; x >= grid.width; x--)
            {
                for(int z = _gridDepth - 1; z >= 0; z--)
                {
                    RemoveNode(x, z);
                }
            }

            _gridWidth = grid.width;
        }

        void UpdateableGrid()
        {
            if(_gridWidth < grid.width)
            {
                AddWidth();
            }

            if(_gridWidth > grid.width && _gridWidth > 0)
            {
                RemoveWidth();
            }

            if(_gridDepth < grid.depth)
            {
                AddDepth();
            }

            if(_gridDepth > grid.depth && _gridDepth > 0)
            {
                RemoveDepth();
            }
        }
    }
}
