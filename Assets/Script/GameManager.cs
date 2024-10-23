using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    ModeMachine modeMachine;

    void Init()
    {
        Command.Init();
        modeMachine = new ModeMachine();
        modeMachine.Init();
    }

    void Awake()
    {
        Init();
    }

    void Start()
    {
        SaveCommand.Instance.LoadLevelFromLocal();
    }

    // Update is called once per frame
    void Update()
    {
        modeMachine.Update();
    }
}
