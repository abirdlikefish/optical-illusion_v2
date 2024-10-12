using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildCommand : Command
{
    private BuildCommand(){}
    private static BuildCommand instance;
    public static BuildCommand Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new BuildCommand();
            }
            return instance;
        }
    }

    public void AddPlayer(Vector3Int position , Vector3Int destination)
    {
        Debug.Log("AddPlayer begin");
        worldSpaceManager.AddPlayer(position, destination);
        Debug.Log("AddPlayer end");
    }
    public bool AddCube(Vector3Int position)
    {
        Debug.Log("AddCube begin");
        if(worldSpaceManager.IsCubeExist(position))
        {
            Debug.LogWarning("AddCube failed");
            return false;
        }
        worldSpaceManager.AddNewCube(position);
        RefreshCameraSpace();
        Debug.Log("AddCube end");
        return true;
    }
    public bool AddCube(Vector3Int position , Vector3Int direction)
    {
        Debug.Log("AddCube_tran begin");
        if(worldSpaceManager.IsCubeExist(position))
        {
            Debug.LogWarning("AddCube failed");
            return false;
        }
        worldSpaceManager.AddNewCube(position, direction);
        RefreshCameraSpace();
        Debug.Log("AddCube_tran end");
        return true;
    }

    public bool AddCube(Vector3Int position, Vector3Int rotateAxis, Vector3 rotateCenter)
    {
        Debug.Log("AddCube_rotate begin");
        if(worldSpaceManager.IsCubeExist(position))
        {
            Debug.LogWarning("AddCube failed");
            return false;
        }
        worldSpaceManager.AddNewCube(position, rotateAxis, rotateCenter);
        RefreshCameraSpace();
        Debug.Log("AddCube_rotate end");
        return true;
    }
    
    public bool RemoveCube(Vector3Int position)
    {
        Debug.Log("RemoveCube begin");
        if(worldSpaceManager.IsCubeExist(position) == false)
        {
            Debug.LogWarning("RemoveCube failed");
            return false;
        }
        worldSpaceManager.RemoveCube(position);
        RefreshCameraSpace();
        Debug.Log("RemoveCube end");
        return true;
    }

    public void AddParentCube(Vector3Int parentPosition, Vector3Int childPosition)
    {
        Debug.Log("AddParentCube begin");
        worldSpaceManager.AddParentCube(parentPosition, childPosition);
        Debug.Log("AddParentCube end");
    }
    public void RemoveParentCube(Vector3Int parentPosition, Vector3Int childPosition)
    {
        Debug.Log("RemoveParentCube begin");
        worldSpaceManager.RemoveParentCube(parentPosition, childPosition);
        Debug.Log("RemoveParentCube end");
    }

    public void SelectCube(Vector3Int position , int index)
    {
        uiManager.SetInput(position , index);
    }
}
