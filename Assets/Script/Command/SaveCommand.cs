using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveCommand : Command
{
    private SaveCommand() { }
    private static SaveCommand instance;
    public static SaveCommand Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SaveCommand();
            }
            return instance;
        }
    }


    public void SaveLevel(int index)
    {
        Debug.Log("Save Level " + index + " begin");
        saveManager.ClearLevelData(index);
        worldSpaceManager.SaveLevelData(
            (x,y) => saveManager.SavePlayer(index , x , y) ,
            (x,y) => saveManager.SaveParentChild(index , x, y) , 
            (x) => saveManager.SaveCube(index,x) , 
            (x,y,z) => saveManager.SaveCubeRotate(index , x , y , z) , 
            (x,y) => saveManager.SaveCubeTranslate(index , x,y)
            );
        saveManager.SaveLevelDataToLocal();
        Debug.Log("Save Level " + index + " end");
    }

    public void LoadLevel(int index)
    {
        Debug.Log("Load Level " + index + " begin");
        worldSpaceManager.ClearWorldSpace();
        saveManager.LoadLevelData(index , worldSpaceManager.AddPlayer , worldSpaceManager.AddParentCube , worldSpaceManager.AddNewCube , worldSpaceManager.AddNewCube , worldSpaceManager.AddNewCube);
        // cameraSpaceManager.ClearNodeMap();
        // worldSpaceManager.DrawGrid(cameraSpaceManager.DrawGrid);
        // cameraSpaceManager.FindPathFromBegPos(worldSpaceManager.GetPlayerPosition());
        RefreshCameraSpace();
        uiManager.SetLevelIndex(index);
        Debug.Log("Load Level " + index + " end");
    }

    public void LoadLevelFromLocal()
    {
        Debug.Log("Load Level From Local begin");
        saveManager.LoadLevelDataFromLocal();
        int levelDataCount = saveManager.GetLevelDataCount();
        uiManager.SetMaxLevelIndex(levelDataCount - 1);
        if(levelDataCount > 0)
        {
            Debug.Log("Load " + levelDataCount + " Levels From Local");
            LoadLevel(0);
        }
        else
        {
            Debug.Log("No Level Data Found");
            CreateNewLevel();
        }
        Debug.Log("Load Level From Local end");
    }

    public void CreateNewLevel()
    {
        Debug.Log("Create New Level " + saveManager.GetLevelDataCount() + " begin");
        saveManager.CreateNewLevelData();
        uiManager.SetMaxLevelIndex(saveManager.GetLevelDataCount() - 1);
        LoadLevel(saveManager.GetLevelDataCount() - 1);
        Debug.Log("Create New Level end");
    }
}
