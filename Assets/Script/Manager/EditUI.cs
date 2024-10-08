using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditUI : MonoBehaviour
{
    private static int editUICnt = 0;
    public static EditUI Init(Action<int>SetLevelIndexToManager , Action<EditObject> SetEditObjectToManager, Action<Vector3>[] SetInputToManager, Action<OperateType> SetOperateToManager)
    {
        if(editUICnt > 0){return null;}
        editUICnt ++;

        GameObject gameObject = new GameObject("EditUI");
        EditUI editUI = gameObject.AddComponent<EditUI>();
        editUI.SetLevelIndexToManager = SetLevelIndexToManager;
        editUI.SetEditObjectToManager = SetEditObjectToManager;
        editUI.SetInputToManager = SetInputToManager;
        editUI.SetOperateToManager = SetOperateToManager;

        editUI.editObject = new string[5]{"cube_player", "Cube" ,  "cube_tran", "cube_rota" , "parent_child"};
        editUI.inputString = new string[9]{"" , "" , "" , "" , "" , "" , "" , "" , ""};


        return editUI;
    }
    private int levelIndex;
    private int editObjectIndex;
    private string[] editObject;
    //  = new string[5]{"cube_player", "Cube" ,  "cube_tran", "cube_rota" , "parent_child"};

    private string[] inputString;
    //  = new string[9];
    public enum EditObject
    {
        CubePlayer,
        Cube,
        CubeTran,
        CubeRota,
        ParentChild
    }
    public enum OperateType
    {
        Add,
        Delete,
        NewLevel,
        SaveLevel
    }

    Action<int> SetLevelIndexToManager;
    Action<EditObject> SetEditObjectToManager;
    Action<Vector3>[] SetInputToManager;
    Action<OperateType> SetOperateToManager;

    public void SetLevelIndexToUI(int index)
    {
        levelIndex = index;
    }
    public void SetEditObjectToUI(EditObject editObject)
    {
        editObjectIndex = (int)editObject;
    }
    public void SetInputToUI(Vector3 num , int index)
    {
        inputString[index] = num.x.ToString();
        inputString[index + 1] = num.y.ToString();
        inputString[index + 2] = num.z.ToString();
    }
    public void UseEditUI()
    {
        gameObject.SetActive(true);
    }
    public void CloseEditUI()
    {
        gameObject.SetActive(false);
    }

    void OnGUI()
    {
        DrawLevelSelection();
        DrawEditObject();
        DrawInputField();
        DrawOperateButton();
    }

  private void DrawLevelSelection()
    {
        GUILayout.BeginHorizontal();
        if(GUILayout.Button("LastLevel"))
        {
            SetLevelIndexToManager(levelIndex - 1);
        }
        GUILayout.Label("Level " + levelIndex.ToString());
        if(GUILayout.Button("NextLevel"))
        {
            SetLevelIndexToManager(levelIndex + 1);
        }
        GUILayout.EndHorizontal();
    }
    private void DrawEditObject()
    {
        editObjectIndex = GUILayout.SelectionGrid(editObjectIndex, editObject, 5);
        SetEditObjectToManager((EditObject)editObjectIndex);
    }
    private void DrawInputField()
    {
        int inputFieldRow ;
        switch(editObjectIndex)
        {
            case 0:
                inputFieldRow = 2;
                break;
            case 1:
                inputFieldRow = 1;
                break;
            case 2:
                inputFieldRow = 2;
                break;
            case 3:
                inputFieldRow = 3;
                break;
            case 4:
                inputFieldRow = 2;
                break;
            default:
                inputFieldRow = 0;
                break;
        }
        for(int i = 0 ; i < inputFieldRow ; i ++)
        {
            GUILayout.BeginHorizontal();
            for(int j = 0 ; j < 3 ; j ++)
            {
                inputString[i * 3 + j] = GUILayout.TextField(inputString[i * 3 + j]);
            }
            GUILayout.EndHorizontal();
        }
    }
    private void DrawOperateButton()
    {
        GUILayout.BeginHorizontal();
        if(GUILayout.Button("Add"))
        {
            SendInputToManager();
            SetOperateToManager(OperateType.Add);
        }
        if(GUILayout.Button("Delete"))
        {
            SendInputToManager();
            SetOperateToManager(OperateType.Delete);
        }
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        if(GUILayout.Button("NewLevel"))
        {
            SetOperateToManager(OperateType.NewLevel);
        }
        if(GUILayout.Button("SaveLevel"))
        {
            SetOperateToManager(OperateType.SaveLevel);
        }
        GUILayout.EndHorizontal();
    }

    private void SendInputToManager()
    {
        for(int i = 0 ; i < 9 ; i ++)
        {
            if(inputString[i] == "")
            {
                inputString[i] = "0";
            }
        }
        for(int i = 0 ; i < 3 ; i ++)
        {
            Vector3 input = new Vector3(float.Parse(inputString[i * 3]), float.Parse(inputString[i * 3 + 1]), float.Parse(inputString[i * 3 + 2]));
            SetInputToManager[i](input);
        }
    }
  
}
