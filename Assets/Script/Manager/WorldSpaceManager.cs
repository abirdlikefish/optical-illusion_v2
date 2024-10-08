using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpaceManager : ICubeMap
{
    #region cubeMap
    private Dictionary<Vector3Int, BaseCube> cubeMap;

    public void SetCubeMap(BaseCube cube, Vector3Int position)
    {
        cubeMap.Add(position, cube);
    }

    public void RemoveCubeMap(Vector3Int position)
    {
        cubeMap.Remove(position);
    }

    public BaseCube GetCube(Vector3Int position)
    {
        return cubeMap[position];
    }

    #endregion
    public bool IsCubeExist(Vector3Int position)
    {
        return cubeMap.ContainsKey(position);
    }

    private HashSet<BaseCube> cubeList;
    private Cube_player player;

    public Vector3Int GetPlayerPosition()
    {
        return player.position;
    }

    public void AddPlayer(Vector3Int position, Vector3Int destination)
    {
        if(player != null)
        {
            player.RemoveCube();
        }
        player = Cube_player.AddPlayer(position, destination);
        if(IsCubeExist(position))
        {
            BaseCube cube = GetCube(position);
            AddParentCubeToPlayer(cube);
        }
    }
    public void AddNewCube(Vector3Int position)
    {
        Cube cube = Cube.AddNewCube(position);
        cubeList.Add(cube);
    }
    public void AddNewCube(Vector3Int position, Vector3Int translateDirection)
    {
        Cube_tran cube = Cube_tran.AddNewCube(position, translateDirection);
        cubeList.Add(cube);
    }
    public void AddNewCube(Vector3Int position, Vector3Int rotateAxis, Vector3 rotateCenter)
    {
        Cube_rota cube = Cube_rota.AddNewCube(position, rotateAxis, rotateCenter);
        cubeList.Add(cube);
    }
    public void RemoveCube(Vector3Int position)
    {
        BaseCube cube = GetCube(position);
        RemoveCube(cube);
    }
    public void RemoveCube(BaseCube cube)
    {
        cubeList.Remove(cube);
        cube.RemoveCube();
    }
    public void AddParentCubeToPlayer(BaseCube parentCube)
    {
        player.AddParentCube(parentCube);
        parentCube.AddChildCube(player);

    }
    public void AddParentCube(Vector3Int parentPosition, Vector3Int childPosition)
    {
        BaseCube parentCube = GetCube(parentPosition);
        BaseCube childCube = GetCube(childPosition);
        parentCube.AddChildCube(childCube);
        childCube.AddParentCube(parentCube);
    }
    public void RemoveParentCube(Vector3Int parentPosition, Vector3Int childPosition)
    {
        BaseCube parentCube = GetCube(parentPosition);
        BaseCube childCube = GetCube(childPosition);
        parentCube.RemoveChildCube(childCube);
        childCube.RemoveParentCube();
    }
    public void DrawGrid(Action<BaseCube> drawCube)
    {
        foreach (BaseCube cube in cubeList)
        {
            drawCube(cube);
        }
    }
    public void SaveLevelData(Action<Vector3Int , Vector3Int> addPlayer , Action<Vector3Int, Vector3Int> addParentChild, Action<Vector3Int> addCube, Action<Vector3Int, Vector3Int, Vector3> addCubeRotate, Action<Vector3Int, Vector3Int> addCubeTranslate)
    {
        player.Save(addPlayer);
        foreach (BaseCube cube in cubeList)
        {
            if (cube is Cube_rota)
            {
                Cube_rota cube_Rota = cube as Cube_rota;
                cube_Rota.Save(addCubeRotate , addParentChild);
            }
            else if (cube is Cube_tran)
            {
                Cube_tran cube_Tran = cube as Cube_tran;
                cube_Tran.Save(addCubeTranslate , addParentChild);
            }
            else if (cube is Cube)
            {
                Cube cube_Normal = cube as Cube;
                cube_Normal.Save(addCube , addParentChild);
            }
            else
            {
                Debug.LogError("SaveLevelData Error");
            }
        }
    }
    public void ClearWorldSpace()
    {
        foreach(BaseCube cube in cubeList)
        {
            cube.RemoveCube();
        }
        if(player != null)
            player.RemoveCube();
        player = null;
        cubeMap.Clear();
        cubeList.Clear();
    }
    public void CubeMove(BaseCube selectedCube)
    {
        selectedCube.Use();
    }
    public void PlayerMove(Action MoveEnd)
    {
        player.Move(MoveEnd);
    }
    public void SetPlayerPath(Vector3Int position)
    {
        player.AddMoveTarget(position);
    }
    public void Init()
    {
        cubeMap = new Dictionary<Vector3Int, BaseCube>();
        cubeList = new HashSet<BaseCube>();
        BaseCube.cubeMap = this;
    }
}
