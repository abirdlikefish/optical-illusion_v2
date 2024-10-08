using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    EditUI editUI;
    public void Init()
    {
        editUI = EditUI.Init(SetLevelIndexToManager , SetEditObjectToManager , new Action<Vector3>[] {SetInputToManager_0, SetInputToManager_1, SetInputToManager_2} , SetOperateToManager);
        SetUIMode(UIMode.Edit);
    }
    public enum UIMode
    {
        Edit,
        Play
    }
    private UIMode uiMode;
    public void SetUIMode(UIMode uiMode)
    {
        this.uiMode = uiMode;
        if(uiMode == UIMode.Edit)
        {
            editUI.UseEditUI();
        }
        else if(uiMode == UIMode.Play)
        {
            editUI.CloseEditUI();
        }
    }
    public void SetUIMode()
    {
        if(uiMode == UIMode.Edit)
        {
            SetUIMode(UIMode.Play);
        }
        else if(uiMode == UIMode.Play)
        {
            SetUIMode(UIMode.Edit);
        }
    }

    private int levelIndex;
    private int maxLevelIndex;
    private EditUI.EditObject editObject;
    private Vector3[] inputVector = new Vector3[3];

#region EditUI delegate
    public void SetLevelIndexToManager(int index)
    {
        if(index < 0)
        {
            levelIndex = maxLevelIndex;
        }
        else if(index > maxLevelIndex)
        {
            levelIndex = 0;
        }
        else
        {
            levelIndex = index;
        }
        SaveCommand.Instance.LoadLevel(levelIndex);
        // return levelIndex;
    }
    public void SetEditObjectToManager(EditUI.EditObject editObject)
    {
        this.editObject = editObject;
    }
    public void SetInputToManager_0(Vector3 num)
    {
        inputVector[0] = num;
    }
    public void SetInputToManager_1(Vector3 num)
    {
        inputVector[1] = num;
    }
    public void SetInputToManager_2(Vector3 num)
    {
        inputVector[2] = num;
    }
    public void SetOperateToManager(EditUI.OperateType operateType)
    {
        switch (operateType)
        {
            case EditUI.OperateType.Add:

                if(editObject == EditUI.EditObject.CubePlayer)
                    BuildCommand.Instance.AddPlayer(Vector3Int.RoundToInt(inputVector[0]), Vector3Int.RoundToInt(inputVector[1]));
                if(editObject == EditUI.EditObject.Cube)
                    BuildCommand.Instance.AddCube(Vector3Int.RoundToInt(inputVector[0]));
                else if(editObject == EditUI.EditObject.CubeTran)
                    BuildCommand.Instance.AddCube(Vector3Int.RoundToInt(inputVector[0]), Vector3Int.RoundToInt(inputVector[1]));
                else if(editObject == EditUI.EditObject.CubeRota)
                    BuildCommand.Instance.AddCube(Vector3Int.RoundToInt(inputVector[0]), Vector3Int.RoundToInt(inputVector[1]), inputVector[2]);
                else if(editObject == EditUI.EditObject.ParentChild)
                    BuildCommand.Instance.AddParentCube(Vector3Int.RoundToInt(inputVector[0]), Vector3Int.RoundToInt(inputVector[1]));
                break;

            case EditUI.OperateType.Delete:

                if(editObject == EditUI.EditObject.ParentChild)
                    BuildCommand.Instance.RemoveParentCube(Vector3Int.RoundToInt(inputVector[0]), Vector3Int.RoundToInt(inputVector[1]));
                else
                    BuildCommand.Instance.RemoveCube(Vector3Int.RoundToInt(inputVector[0]));
                break;
                
            case EditUI.OperateType.NewLevel:
                SaveCommand.Instance.CreateNewLevel();
                break;
            case EditUI.OperateType.SaveLevel:
                SaveCommand.Instance.SaveLevel(levelIndex);
                break;
        }
    }

#endregion

    public void SetMaxLevelIndex(int index)
    {
        maxLevelIndex = index;
    }
    public void SetLevelIndex(int index)
    {
        levelIndex = index;
        editUI.SetLevelIndexToUI(index);
    }
    public void SetInput(Vector3 num , int index)
    {
        editUI.SetInputToUI(num , index);
    }
    public void SetEditObject(EditUI.EditObject editObject)
    {
        editUI.SetEditObjectToUI(editObject);
    }



}
