using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITransmitData_editUI2UIManager
{
    public void SetLevelIndexToManager(int index);
    public void SetEditObjectToManager(EditUI.EditObject editObject);
    // public void SetInputToManager_0(Vector3 num);
    // public void SetInputToManager_1(Vector3 num);
    // public void SetInputToManager_2(Vector3 num);
    public void SetInputToManager(Vector3 num, int index);
    public void SetOperateToManager(EditUI.OperateType operateType);
}
