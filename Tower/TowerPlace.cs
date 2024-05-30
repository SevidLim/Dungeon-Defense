using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlace : MonoBehaviour
{
    public bool IsPlacing;

    void Update()
    {
        if (!IsPlacing)
        {
            this.gameObject.tag = "Tower_Place";
        }
    }
}
