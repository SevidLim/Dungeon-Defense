using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveList : MonoBehaviour
{
    public GameObject _UI;
    public GameObject[] UIOff;

    public void UIOnOff()
    {
        _UI.SetActive(!_UI.activeSelf);

        foreach (GameObject obj in UIOff)
        {
            obj.SetActive(false);
        }
    }
}
