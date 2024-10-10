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
            // EditCommand.Instance
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
