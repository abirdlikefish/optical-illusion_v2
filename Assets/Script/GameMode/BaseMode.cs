using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMode
{
    static protected ModeMachine modeMachine;
    static public void Init(ModeMachine modeMachine)
    {
        BaseMode.modeMachine = modeMachine;
    }
    public virtual void Enter()
    {

    }
    public virtual void Update()
    {

    }
    public virtual void Exit()
    {

    }
}
