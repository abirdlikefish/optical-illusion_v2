using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command
{
    protected static WorldSpaceManager worldSpaceManager;
    protected static CameraSpaceManager cameraSpaceManager;
    protected static UIManager uiManager;
    protected static SaveManager saveManager;
    protected static CameraManager cameraManager;

    public static void Init()
    {
        Camera mainCamera = Camera.main;
        if(mainCamera == null)
            Debug.Log("Main Camera is not found");
        else
            cameraManager = mainCamera.gameObject.AddComponent<CameraManager>();
        cameraManager.Init(0);

        worldSpaceManager = new WorldSpaceManager();
        cameraSpaceManager = new CameraSpaceManager();
        uiManager = new UIManager();
        saveManager = new SaveManager();

        worldSpaceManager.Init();
        cameraSpaceManager.Init();
        uiManager.Init();
        saveManager.Init();

    }
    protected void RefreshCameraSpace()
    {
        cameraSpaceManager.ClearNodeMap();
        worldSpaceManager.DrawGrid(cameraSpaceManager.DrawGrid);
        cameraSpaceManager.FindPathFromBegPos(worldSpaceManager.GetPlayerPosition());
    }

}
