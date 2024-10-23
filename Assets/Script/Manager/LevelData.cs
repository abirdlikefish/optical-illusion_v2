using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public int index;
    [System.Serializable]
    struct s_Player
    {
        public Vector3Int position;
        public Vector3Int destination;
    }
    [System.Serializable]
    struct s_ParentChild
    {
        public Vector3Int parentPosition;
        public Vector3Int childPosition;
    }
    [System.Serializable]
    struct s_Cube
    {
        public Vector3Int position;
    }
    [System.Serializable]
    struct s_Cube_rotate
    {
        public Vector3Int position;
        public Vector3 rotateCenter;
        public Vector3Int rotateAxis;
    }
    [System.Serializable]
    struct s_Cube_translate
    {
        public Vector3Int position;
        public Vector3Int translateDirection;
    }
    [SerializeField]
    private s_Player player;
    [SerializeField]
    private List<s_ParentChild> parentChildList;
    [SerializeField]
    private List<s_Cube> cubeList;
    [SerializeField]
    private List<s_Cube_rotate> cubeRotateList;
    [SerializeField]
    private List<s_Cube_translate> cubeTranslateList;
    public void AddPlayer(Vector3Int position, Vector3Int destination)
    {
        player.position = position;
        player.destination = destination;
    }
    public void AddParentChild(Vector3Int parentPosition, Vector3Int childPosition)
    {
        s_ParentChild parentChild = new s_ParentChild();
        parentChild.parentPosition = parentPosition;
        parentChild.childPosition = childPosition;
        parentChildList.Add(parentChild);
    }
    public void AddCube(Vector3Int position)
    {
        s_Cube cubeRotate = new s_Cube();
        cubeRotate.position = position;
        cubeList.Add(cubeRotate);
    }
    public void AddCubeRotate(Vector3Int position, Vector3Int rotateAxis, Vector3 rotateCenter)
    {
        s_Cube_rotate cubeRotate = new s_Cube_rotate();
        cubeRotate.position = position;
        cubeRotate.rotateCenter = rotateCenter;
        cubeRotate.rotateAxis = rotateAxis;
        cubeRotateList.Add(cubeRotate);
    }
    public void AddCubeTranslate(Vector3Int position, Vector3Int translateDirection)
    {
        s_Cube_translate cubeTranslate = new s_Cube_translate();
        cubeTranslate.position = position;
        cubeTranslate.translateDirection = translateDirection;
        cubeTranslateList.Add(cubeTranslate);
    }
    public void ClearLevelData()
    {
        parentChildList.Clear();
        cubeList.Clear();
        cubeRotateList.Clear();
        cubeTranslateList.Clear();
    }
    public void LoadLevelData(int index ,Action<Vector3Int , Vector3Int> addPlayer , Action<Vector3Int, Vector3Int> addParentChild, Action<Vector3Int> addCube, Action<Vector3Int, Vector3Int, Vector3> addCubeRotate, Action<Vector3Int, Vector3Int> addCubeTranslate)
    {
        this.index = index;
        addPlayer(player.position, player.destination);
        foreach (s_Cube cube in cubeList)
        {
            addCube(cube.position);
        }
        foreach (s_Cube_rotate cubeRotate in cubeRotateList)
        {
            addCubeRotate(cubeRotate.position, cubeRotate.rotateAxis, cubeRotate.rotateCenter);
        }
        foreach (s_Cube_translate cubeTranslate in cubeTranslateList)
        {
            addCubeTranslate(cubeTranslate.position, cubeTranslate.translateDirection);
        }
        foreach (s_ParentChild parentChild in parentChildList)
        {
            addParentChild(parentChild.parentPosition, parentChild.childPosition);
        }
    }
    public void Init()
    {
        cubeList = new List<s_Cube>();
        parentChildList = new List<s_ParentChild>();
        cubeRotateList = new List<s_Cube_rotate>();
        cubeTranslateList = new List<s_Cube_translate>();
    }
    public void Show()
    {
        Debug.Log("player: " + player.position + " " + player.destination);
        foreach (s_ParentChild parentChild in parentChildList)
        {
            Debug.Log("parentChild: " + parentChild.parentPosition + " " + parentChild.childPosition);
        }
        foreach (s_Cube cube in cubeList)
        {
            Debug.Log("cube: " + cube.position);
        }
        foreach (s_Cube_rotate cubeRotate in cubeRotateList)
        {
            Debug.Log("cubeRotate: " + cubeRotate.position + " " + cubeRotate.rotateAxis + " " + cubeRotate.rotateCenter);
        }
        foreach (s_Cube_translate cubeTranslate in cubeTranslateList)
        {
            Debug.Log("cubeTranslate: " + cubeTranslate.position + " " + cubeTranslate.translateDirection);
        }
    }
}
