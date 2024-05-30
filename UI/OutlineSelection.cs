using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OutlineSelection : MonoBehaviour
{
    public GameObject towerUI;
    public GameObject _state;
    public GameObject _range;

    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;

    void Update()
    {
        // Highlight
        if (highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;

        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit))
        {
            highlight = raycastHit.transform;
            if (highlight.CompareTag("Tower") && highlight != selection)
            {
                if (highlight.gameObject.GetComponent<Outline>() != null)
                {
                    highlight.gameObject.GetComponent<Outline>().enabled = true;
                }
                else
                {
                    Outline outline = highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                    highlight.gameObject.GetComponent<Outline>().OutlineColor = new Color(0.5f, 1f, 0.23f, 0.1f);
                    highlight.gameObject.GetComponent<Outline>().OutlineWidth = 7.0f;
                }
            }
            else
            {
                highlight = null;
            }
        }

        // Selection
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit))
            {
                if (highlight)
                {
                    if (selection != null)
                    {
                        selection.gameObject.GetComponent<Outline>().enabled = false;
                        towerUI.SetActive(false);
                        _range.SetActive(false);
                    }
                    selection = raycastHit.transform;
                    selection.gameObject.GetComponent<Outline>().enabled = true;

                    Transform[] childObjects = selection.GetComponentsInChildren<Transform>(true);

                    foreach (Transform child in childObjects)
                    {
                        if (child.gameObject.name == "TowerCanvas")
                        {
                            towerUI = child.gameObject;
                        }
                        else if (child.gameObject.name == "show range")
                        {
                            _range = child.gameObject;
                        }
                    }

                    towerUI.SetActive(true);
                    _range.SetActive(true);

                    highlight = null;
                }
                else
                {
                    if (selection)
                    {
                        towerUI.SetActive(false);
                        _range.SetActive(false);
                        selection.gameObject.GetComponent<Outline>().enabled = false;
                        selection = null;
                        towerUI = null;
                        _range = null;
                    }
                }
            }
        }
    }
}
