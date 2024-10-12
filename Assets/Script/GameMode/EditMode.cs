using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditMode : BaseMode
{
    public override void Update()
    {
        BaseCube selectedCube = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10000, LayerMask.GetMask("BaseCube")))
        {
            selectedCube = hit.collider.GetComponent<BaseCube>();
        }
        
        if (Input.GetMouseButtonDown(0) && selectedCube != null)
        {
            BuildCommand.Instance.SelectCube(selectedCube.position , 0);
        }
        if (Input.GetMouseButtonDown(1) && selectedCube != null)
        {
            BuildCommand.Instance.SelectCube(selectedCube.position , 1);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            BuildCommand.Instance.SelectNextCube(Vector3Int.up , 0);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            BuildCommand.Instance.SelectNextCube(Vector3Int.down , 0);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            BuildCommand.Instance.SelectNextCube(Vector3Int.right , 0);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            BuildCommand.Instance.SelectNextCube(Vector3Int.left , 0);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            BuildCommand.Instance.SelectNextCube(Vector3Int.back , 0);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            BuildCommand.Instance.SelectNextCube(Vector3Int.forward , 0);
        }

        if(Input.GetKeyDown(KeyCode.CapsLock))
        {
            SystemCommand.Instance.ChangeGameMode();
            modeMachine.SwitchMode();
        }
        else if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            PlayCommand.Instance.ChangeCameraDirection();
        }
    }
}
