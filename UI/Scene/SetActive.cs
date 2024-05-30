using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActive : MonoBehaviour
{
    public GameObject _UI;

    public void UIOnOff()
    {
        _UI.SetActive(!_UI.activeSelf);
    }

    public void pauseGame()
    {
        if (Time.timeScale == 1.0f)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
