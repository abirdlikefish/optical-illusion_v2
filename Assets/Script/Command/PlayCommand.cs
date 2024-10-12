using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCommand : Command
{
    private PlayCommand(){}
    private static PlayCommand instance;
    public static PlayCommand Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayCommand();
            }
            return instance;
        }
    }

    public void ChangeCameraDirection()
    {
        Debug.Log("ChangeCameraDirection begin");
        cameraManager.ChangeDirection();
        cameraSpaceManager.ChangeCameraDirection();
        // cameraSpaceManager.ClearNodeMap();
        // worldSpaceManager.DrawGrid(cameraSpaceManager.DrawGrid);
        // cameraSpaceManager.FindPathFromBegPos(worldSpaceManager.GetPlayerPosition());
        RefreshCameraSpace();
        Debug.Log("ChangeCameraDirection end");
    }

    public bool PlayerMove(BaseCube selectedCube , Action<bool> Lock)
    {
        Debug.Log("PlayerMove begin");
        bool isAccessible= cameraSpaceManager.GetPath(selectedCube.position , worldSpaceManager.SetPlayerPath);
        if(isAccessible)
        {
            Lock(true);
            worldSpaceManager.PlayerMove(() => 
            {
                cameraSpaceManager.FindPathFromBegPos(worldSpaceManager.GetPlayerPosition());
                worldSpaceManager.AddParentCubeToPlayer(selectedCube);
                Debug.Log("PlayerMove end");
                Lock(false);
            });
        }
        else
            Debug.Log("PlayerMove failed , unaccessible to " + selectedCube.position);
        return isAccessible;
    }

    public void CubeMove(BaseCube selectedCube)
    {
        Debug.Log("CubeMove begin");
        worldSpaceManager.CubeMove(selectedCube);
        RefreshCameraSpace();
        Debug.Log("CubeMove end");
    }

    public void JudgeVictory(bool flag)
    {
        if(flag)
        {
            Debug.Log("Victory " + uiManager.levelIndex);
            Debug.Log("Next Level " + uiManager.GetNextLevelIndex());
            SaveCommand.Instance.LoadLevel(uiManager.GetNextLevelIndex());
        }
    }

}