using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUI 
{
    GameObject gameRoot { get; }

    bool CanCloseWithback {  get; }

    void OnOpen();
    void OnClose();

    void SetInteractable(bool value);
}
