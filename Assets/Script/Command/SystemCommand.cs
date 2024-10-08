using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemCommand : Command
{
    private SystemCommand() { }
    private static SystemCommand instance;
    public static SystemCommand Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SystemCommand();
            }
            return instance;
        }
    }

    public void ChangeGameMode()
    {
        Debug.Log("Change Game Mode begin");

        SaveCommand.Instance.LoadLevel(0);
        uiManager.SetUIMode();

        Debug.Log("Change Game Mode end");
    }


}
