using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;

// Concrete States
public class BoyDialogState : IDialogState
{
    private DialogManager manager;
    private string dialog;

    public BoyDialogState(DialogManager manager, string dialog)
    {
        this.manager = manager;
        this.dialog = dialog;
    }

    public void Enter()
    {
        Debug.Log("Boy: " + dialog);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            manager.NextState();
        }
    }

    public void Exit() { }
}