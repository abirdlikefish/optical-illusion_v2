using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_tran : BaseCube
{
    public static Cube_tran AddNewCube(Vector3Int position, Vector3Int translateDirection)
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Cube_tran");
        if(prefab == null)
        {
            Debug.LogError("Cube_tran prefab not found");
            return null;
        }
        prefab = Instantiate(prefab);
        Cube_tran cube = prefab.GetComponent<Cube_tran>();
        // Cube_tran cube = Instantiate(Resources.Load<GameObject>("Prefabs/Cube_tran")).GetComponent<Cube_tran>();
        cube.Init(position, translateDirection);
        return cube;
    }
    protected Vector3Int translateDirection;
    protected override void ChangePosition()
    {
        cubeMap.RemoveCubeMap(position);
        position += translateDirection;
        transform.position = position;
        cubeMap.SetCubeMap(this, position);
    }

    protected void Init(Vector3Int position, Vector3Int translateDirection)
    {
        this.position = position;
        this.translateDirection = translateDirection;
        transform.position = position;
        cubeMap.SetCubeMap(this, position);

        childCubes = new HashSet<BaseCube>();
    }

    public void Save(Action<Vector3Int, Vector3Int> addCubeTranslate, Action<Vector3Int, Vector3Int> addCubeParentChild)
    {
        addCubeTranslate(position, translateDirection);
        if (parentCube != null)
        {
            addCubeParentChild(parentCube.position, position);
        }
    }

}
