using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraSpaceManager
{
    static Vector3Int[] cameraDirection = new Vector3Int[2]{new Vector3Int(1, 1, 1), new Vector3Int(-1, 1, 1)};
    static int cameraDirectionIndex;
    enum NodeType
    {
        top,
        side
    }
    struct HalfNode
    {
        public NodeType type;
        public BaseCube cube;
        public int height{get{return cube == null ? -10000000 : (int)Vector3.Dot(cube.position , cameraDirection[cameraDirectionIndex]); }}
        public void SetHalfNode(NodeType type, BaseCube cube)
        {
            this.type = type;
            this.cube = cube;
        }
        public void clear()
        {
            type = NodeType.side;
            cube = null;
        }
    }
    struct Node
    {
        public HalfNode leftNode;
        public HalfNode rightNode;
        public bool isPassable{get{return leftNode.type == NodeType.top && rightNode.type == NodeType.top;}}
        public Vector3Int position{get{return leftNode.height > rightNode.height ? leftNode.cube.position : rightNode.cube.position;}}
    }

    private Dictionary<Vector2Int, Node> nodeMap;
    public void ClearNodeMap()
    {
        Debug.Log("ClearNodeMap");
        nodeMap.Clear();
    }
    public void DrawGrid(BaseCube cube)
    {
        Vector2Int[,] offset = new Vector2Int[2,2]{{Vector2Int.right, Vector2Int.up}, {Vector2Int.up, Vector2Int.left}};
        Vector3Int mid = cube.position;
        mid -= mid.y * cameraDirection[cameraDirectionIndex];
        Vector2Int pos = new Vector2Int(mid.x, mid.z);
        // Debug.Log("DrawGrid " + pos);

        DrawGrid_L(pos, cube, NodeType.top);
        DrawGrid_R(pos, cube, NodeType.top);

        DrawGrid_L(pos + offset[cameraDirectionIndex,0] + offset[cameraDirectionIndex , 1], cube, NodeType.side);
        DrawGrid_R(pos + offset[cameraDirectionIndex,0] + offset[cameraDirectionIndex , 1], cube, NodeType.side);

        DrawGrid_L(pos + offset[cameraDirectionIndex,1], cube, NodeType.side);
        DrawGrid_R(pos + offset[cameraDirectionIndex,0], cube, NodeType.side);
        // Debug.Log("show grid");
        // Debug.Log("nodeMap size = " + nodeMap.Count);

    }
    void DrawGrid_L(Vector2Int pos, BaseCube cube , NodeType type)
    {
        
        HalfNode node = new HalfNode();
        node.SetHalfNode(type, cube);
        // Debug.Log("pos = " + pos + " node.height = " + node.height);
        if(nodeMap.ContainsKey(pos) && nodeMap[pos].leftNode.height > node.height)
        {
            // Debug.Log("nodeMap[pos].leftNode.height = " + nodeMap[pos].leftNode.height);
        }
        else
        {
            if(nodeMap.ContainsKey(pos))
            {
                Node tempNode = nodeMap[pos];
                tempNode.leftNode = node;
                nodeMap[pos] = tempNode;
            }
            else
            {
                Node tempNode = new Node();
                tempNode.leftNode = node;
                tempNode.rightNode.clear();
                nodeMap.Add(pos, tempNode);
            }
        }
        // Debug.Log("DrawGrid " + pos);
    }
    void DrawGrid_R(Vector2Int pos, BaseCube cube , NodeType type)
    {
        HalfNode node = new HalfNode();
        node.SetHalfNode(type, cube);
        // Debug.Log("pos = " + pos + " node.height = " + node.height);
        if(nodeMap.ContainsKey(pos) && nodeMap[pos].rightNode.height > node.height)
        {
            // Debug.Log("nodeMap[pos].rightNode.height = " + nodeMap[pos].rightNode.height);
        }
        else
        {
            if(nodeMap.ContainsKey(pos))
            {
                Node tempNode = nodeMap[pos];
                tempNode.rightNode = node;
                nodeMap[pos] = tempNode;
            }
            else
            {
                Node tempNode = new Node();
                tempNode.rightNode = node;
                tempNode.leftNode.clear();
                nodeMap.Add(pos, tempNode);
            }
        }
        // Debug.Log("DrawGrid " + pos);
    }
    private Dictionary<Vector2Int , Vector2Int> lastPosition;

    public void FindPathFromBegPos(Vector3Int begPos)
    {
        begPos -= begPos.y * cameraDirection[cameraDirectionIndex];
        FindPathFromBegPos(new Vector2Int(begPos.x, begPos.z));
    }
    public void FindPathFromBegPos(Vector2Int begPos)
    {
        lastPosition.Clear();
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        queue.Enqueue(begPos);
        lastPosition.Add(begPos, begPos);
        Vector2Int[] offset = new Vector2Int[4]{Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right};
        while(queue.Count > 0)
        {
            Vector2Int pos = queue.Dequeue();
            for(int i = 0 ; i < 4 ; i ++)
            {
                Vector2Int nextPos = pos + offset[i];
                // if(nodeMap.ContainsKey(nextPos))
                // {
                //     Debug.Log("nextPos = " + nextPos);
                //     Debug.Log("nodeMap[nextPos].isPassable = " + nodeMap[nextPos].isPassable);
                // }
                if(!lastPosition.ContainsKey(nextPos) && nodeMap.ContainsKey(nextPos) && nodeMap[nextPos].isPassable)
                {
                    queue.Enqueue(nextPos);
                    lastPosition.Add(nextPos, pos);
                }
            }
        }
        // Debug.Log("show grid");
        // Debug.Log("nodeMap size = " + nodeMap.Count);
        // foreach(var i in nodeMap)
        // {
        //     Debug.Log("nodeMap key = " + i.Key + " nodeMap height = " + i.Value.leftNode.height + " " + i.Value.rightNode.height + " isPassable = " + i.Value.isPassable + " nodeMap type = " + i.Value.leftNode.type + " " + i.Value.rightNode.type);
        // }
    }

    public  bool GetPath(Vector3Int endPos ,Action<Vector3Int> drawPath )
    {
        endPos -= endPos.y * cameraDirection[cameraDirectionIndex];
        return GetPath(new Vector2Int(endPos.x, endPos.z), drawPath);
    }
    public bool GetPath(Vector2Int endPos , Action<Vector3Int> drawPath)
    {
        if(!lastPosition.ContainsKey(endPos) || lastPosition[endPos] == endPos)
        {
            return false;
        }
        Vector2Int lefInd = endPos;
        Vector2Int rigInd = lastPosition[endPos];
        Vector3Int lef = nodeMap[lefInd].position;
        Vector3Int rig = nodeMap[rigInd].position;
        if(lef.y > rig.y)
            rig += (lef.y - rig.y) * cameraDirection[cameraDirectionIndex];
        else
            lef += (rig.y - lef.y) * cameraDirection[cameraDirectionIndex];
        while(lefInd != rigInd)
        {
            drawPath(lef);
            drawPath(rig);
            lefInd = rigInd;
            rigInd = lastPosition[rigInd];
            lef = nodeMap[lefInd].position;
            rig = nodeMap[rigInd].position;
            if(lef.y > rig.y)
                rig += (lef.y - rig.y) * cameraDirection[cameraDirectionIndex];
            else
                lef += (rig.y - lef.y) * cameraDirection[cameraDirectionIndex];
        }
        return true;
    }

    public void ChangeCameraDirection()
    {
        cameraDirectionIndex = 1^cameraDirectionIndex;
        Debug.Log("current camera direction = " + cameraDirectionIndex);
    }

    public void Init()
    {
        nodeMap = new Dictionary<Vector2Int, Node>();
        lastPosition = new Dictionary<Vector2Int, Vector2Int>();
    }

}
