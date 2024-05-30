using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressAnyKey : MonoBehaviour
{
    public Button button;
    public KeyCode _Key;

    void Start()
    {
        button = GetComponent<Button>();
    }

    void Update()
    {
        if (Input.GetKeyDown(_Key))
        {
            button.onClick.Invoke();
        }
    }
}
