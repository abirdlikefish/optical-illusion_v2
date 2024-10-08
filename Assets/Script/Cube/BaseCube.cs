using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCube : MonoBehaviour
{
    public static ICubeMap cubeMap;
    protected BaseCube parentCube;
    protected HashSet<BaseCube> childCubes;
    public Vector3Int position { get; protected set; }
    public virtual void Use()
    {
        if(parentCube == null)
        {
            ChangePosition();
            foreach(BaseCube childCube in childCubes)
            {
                childCube.ChangePosition();
            }
        }
        else
        {
            parentCube.Use();
        }
    }
    public virtual void AddParentCube(BaseCube parentCube)
    {
        this.parentCube = parentCube;
    }
    public virtual void AddChildCube(BaseCube childCube)
    {
        if(childCubes.Contains(childCube))
            return;
        childCubes.Add(childCube);
    }
    public virtual void RemoveParentCube()
    {
        parentCube = null;
    }
    public virtual void RemoveChildCube(BaseCube childCube)
    {
        childCubes.Remove(childCube);
    }
    // public virtual void RemoveCube()
    public void RemoveCube()
    {
        cubeMap.RemoveCubeMap(position);
        foreach(BaseCube childCube in childCubes)
        {
            childCube.RemoveParentCube();
        }
        if(parentCube != null)
        {
            parentCube.RemoveChildCube(this);
        }
        Destroy(gameObject);
    }

    protected virtual void ChangePosition()
    {

    }
}
