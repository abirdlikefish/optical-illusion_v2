using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : ITransmitData_editUI2UIManager
{
    EditUI editUI;
    private UIMode uiMode;
    private int maxLevelIndex;
    public int levelIndex{get; private set;}
    private EditUI.EditObject editObject;   //editUI 选中的对象类型
    private Vector3[] inputVector = new Vector3[3];
    public void Init()
    {
        // editUI = EditUI.Init(SetLevelIndexToManager , SetEditObjectToManager , new Action<Vector3>[] {SetInputToManager_0, SetInputToManager_1, SetInputToManager_2} , SetOperateToManager);
        editUI = EditUI.Init(this);
        SetUIMode(UIMode.Edit);
    }
    public enum UIMode
    {
        Edit,
        Play
    }
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

// editUI
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
    }
    public void SetEditObjectToManager(EditUI.EditObject editObject)
    {
        this.editObject = editObject;
    }
    public void SetInputToManager(Vector3 num , int index)
    {
        inputVector[index] = num;
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
        if(uiMode == UIMode.Edit)
        {
            editUI.SetLevelIndexToUI(index);
        }
        else
        {

        }
    }
    public void SetInput(Vector3 num , int index)
    {
        inputVector[index] = num;
        editUI.SetInputToUI(num , index);
        Debug.Log("SetInput " + num);
    }
    public Vector3 GetInput(int index)
    {
        return inputVector[index];
    }
    public void SetEditObject(EditUI.EditObject editObject)
    {
        this.editObject = editObject;
        editUI.SetEditObjectToUI(editObject);
    }
    public int GetNextLevelIndex()
    {
        if(levelIndex == maxLevelIndex)
        {
            return 0;
        }
        else
        {
            return levelIndex + 1;
        }
    }

}
