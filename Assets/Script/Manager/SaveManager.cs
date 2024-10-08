using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager
{
    private List<LevelData> levelDataList;
    public void Init()
    {
        levelDataList = new List<LevelData>();
    }

#region  save
    public void ClearLevelData(int index)
    {
        levelDataList[index].ClearLevelData();
    }
    public void CreateNewLevelData()
    {
        LevelData levelData = new LevelData();
        levelData.Init();
        levelDataList.Add(levelData);
    }
    public void SavePlayer(int index, Vector3Int position, Vector3Int destination)
    {
        levelDataList[index].AddPlayer(position, destination);
    }
    public void SaveParentChild(int index, Vector3Int parentPosition, Vector3Int childPosition)
    {
        levelDataList[index].AddParentChild(parentPosition, childPosition);
    }
    public void SaveCube(int index, Vector3Int position)
    {
        levelDataList[index].AddCube(position);
    }
    public void SaveCubeRotate(int index, Vector3Int position, Vector3Int rotateAxis, Vector3 rotateCenter)
    {
        levelDataList[index].AddCubeRotate(position, rotateAxis, rotateCenter);
    }
    public void SaveCubeTranslate(int index, Vector3Int position, Vector3Int translateDirection)
    {
        levelDataList[index].AddCubeTranslate(position, translateDirection);
    }
#endregion

#region load
    public void LoadLevelData(int index, Action<Vector3Int, Vector3Int> addPlayer, Action<Vector3Int, Vector3Int> addParentChild, Action<Vector3Int> addCube, Action<Vector3Int, Vector3Int, Vector3> addCubeRotate, Action<Vector3Int, Vector3Int> addCubeTranslate)
    {
        levelDataList[index].LoadLevelData(addPlayer , addParentChild, addCube, addCubeRotate, addCubeTranslate);
    }
#endregion

    private static string levelDataPath = "./LevelData/";
    public void SaveLevelDataToLocal()
    {
        for(int i = 0 ; i < levelDataList.Count ; i++)
        {
            levelDataList[i].Show();
            string levelDataJson = JsonUtility.ToJson(levelDataList[i]);
            File.WriteAllText(levelDataPath + i.ToString() + ".json", levelDataJson);
        }
    }
    public void LoadLevelDataFromLocal()
    {
        levelDataList.Clear();
        DirectoryInfo directoryInfo = new DirectoryInfo(levelDataPath);
        FileInfo[] fileInfos = directoryInfo.GetFiles();
        foreach (FileInfo fileInfo in fileInfos)
        {
            string levelDataJson = File.ReadAllText(fileInfo.FullName);
            LevelData levelData = JsonUtility.FromJson<LevelData>(levelDataJson);
            levelDataList.Add(levelData);
        }
    }
    public int GetLevelDataCount()
    {
        return levelDataList.Count;
    }
}
