using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Cube_rota : BaseCube
{
    public static Cube_rota AddNewCube(Vector3Int position, Vector3Int rotateAxis, Vector3 rotateCenter)
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Cube_rota");
        if(prefab == null)
        {
            Debug.LogError("Cube_rota prefab not found");
            return null;
        }
        prefab = Instantiate(prefab);
        Cube_rota cube = prefab.GetComponent<Cube_rota>();
        // Cube_rota cube = Instantiate(Resources.Load<GameObject>("Prefabs/Cube_rota")).GetComponent<Cube_rota>();
        cube.Init(position, rotateAxis, rotateCenter);
        return cube;
    }
    protected Vector3Int rotateAxis;
    protected Vector3 rotateCenter;
    protected override void ChangePosition()
    {
        cubeMap.RemoveCubeMap(position);
        transform.RotateAround(rotateCenter , rotateAxis , 90f);
        position = Vector3Int.RoundToInt(transform.position);
        cubeMap.SetCubeMap(this, position);
    }
    public void Init(Vector3Int position, Vector3Int rotateAxis, Vector3 rotateCenter)
    {
        this.position = position;
        this.rotateAxis = rotateAxis;
        this.rotateCenter = rotateCenter;
        transform.position = position;
        cubeMap.SetCubeMap(this, position);

        childCubes = new HashSet<BaseCube>();
    }
    public void Save(Action<Vector3Int, Vector3Int, Vector3> addCubeRotate , Action<Vector3Int, Vector3Int> addCubeParentChild)
    {
        addCubeRotate(position, rotateAxis, rotateCenter);
        if(parentCube != null)
        {
            addCubeParentChild(parentCube.position, position);
        }
    }
}
