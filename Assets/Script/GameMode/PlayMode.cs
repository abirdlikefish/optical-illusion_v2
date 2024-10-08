using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMode : BaseMode
{
    private bool isLocked;

    public override void Enter()
    {
        base.Enter();
        isLocked = false;
    }
    public override void Update()
    {
        if(isLocked)
            return;
        BaseCube selectedCube = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10000, LayerMask.GetMask("BaseCube")))
        {
            selectedCube = hit.collider.GetComponent<BaseCube>();
        }

        if (Input.GetMouseButtonDown(0) && selectedCube != null)
        {
            PlayCommand.Instance.PlayerMove(selectedCube , Lock);
        }
        else if (Input.GetMouseButtonDown(1) && selectedCube != null)
        {
            PlayCommand.Instance.CubeMove(selectedCube);
        }
        else if(Input.GetKeyDown(KeyCode.CapsLock))
        {
            SystemCommand.Instance.ChangeGameMode();
            modeMachine.SwitchMode();
        }
        else if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            PlayCommand.Instance.ChangeCameraDirection();
        }
    }
    public override void Exit()
    {
        base.Exit();
        isLocked = false;
    }

    public void Lock(bool isLock)
    {
        isLocked = isLock;
    }
}
