using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : BaseCube
{
    public static Cube AddNewCube(Vector3Int position)
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Cube");
        if(prefab == null)
        {
            Debug.LogError("Cube prefab not found");
            return null;
        }
        prefab = Instantiate(prefab);
        Cube cube = prefab.GetComponent<Cube>();
        // Cube cube = Instantiate(Resources.Load<GameObject>("Prefabs/Cube")).GetComponent<Cube>();
        cube.Init(position);
        return cube;
    }

    public void Init(Vector3Int position)
    {
        this.position = position;
        transform.position = position;
        cubeMap.SetCubeMap(this, position);

        childCubes = new HashSet<BaseCube>();
    }

    public void Save(Action<Vector3Int> addCube, Action<Vector3Int, Vector3Int> addCubeParentChild)
    {
        addCube(position);
        if (parentCube != null)
        {
            addCubeParentChild(parentCube.position, position);
        }
    }
}
