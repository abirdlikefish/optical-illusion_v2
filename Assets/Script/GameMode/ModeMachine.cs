using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeMachine
{
    BaseMode currentMode;
    BaseMode editMode;
    BaseMode playMode;

    public void Init()
    {
        BaseMode.Init(this);
        editMode = new EditMode();
        playMode = new PlayMode();
        currentMode = editMode;
        editMode.Enter();
    }

    public void Update()
    {
        currentMode.Update();
    }

    public void SwitchMode()
    {
        currentMode.Exit();
        if (currentMode == editMode)
        {
            currentMode = playMode;
        }
        else
        {
            currentMode = editMode;
        }
        currentMode.Enter();
    }
}
