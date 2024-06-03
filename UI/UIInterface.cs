using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIInterface : MonoBehaviour
{
    private GameObject _tower;
    private GameObject _trap;

    public GameObject TowerPlacing;
    public GameObject TrapPlacing;

    GameObject focusObjs;

    public bool holdingTower = false;
    public bool holdingTrap = false;

    public Money money;
    public TowerPlace towerPlace;

    public GameObject _range;

    void Update()
    {
        if (holdingTower)
        {
            TowerPlacing.SetActive(true);

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (!RaycastWithoutTriggers(ray, out hit))
                {
                    return;
                }
                focusObjs = Instantiate(_tower, hit.point, _tower.transform.rotation);
                DisableColliders();
            }
            else if (Input.GetMouseButton(0) && focusObjs != null)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (!RaycastWithoutTriggers(ray, out hit))
                {
                    return;
                }
                focusObjs.transform.position = hit.point;
            }
            else if (Input.GetMouseButtonUp(0) && focusObjs != null)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (!RaycastWithoutTriggers(ray, out hit))
                {
                    return;
                }
                if (hit.collider.gameObject.CompareTag("Tower_Place") && hit.normal.Equals(new Vector3(0, 1, 0)))
                {
                    hit.collider.gameObject.tag = "occupied";

                    towerPlace = hit.collider.gameObject.GetComponent<TowerPlace>();
                    towerPlace.IsPlacing = true;

                    focusObjs.transform.position = new Vector3(hit.collider.gameObject.transform.position.x, focusObjs.transform.position.y, hit.collider.gameObject.transform.position.z);
                    EnabledColliders();
                    money.payOrNot = true;

                    Transform[] childObjects = focusObjs.GetComponentsInChildren<Transform>(true);

                    Tower tower = focusObjs.GetComponent<Tower>();

                    tower._towerPlace = towerPlace;

                    foreach (Transform child in childObjects)
                    {
                        if (child.gameObject.name == "show range")
                        {
                            _range = child.gameObject;
                        }
                    }

                    _range.SetActive(false);
                }
                else
                {
                    Destroy(focusObjs);
                }
                focusObjs = null;
                holdingTower = false;
                TowerPlacing.SetActive(false);
                towerPlace = null;
            }
        }

        if (holdingTrap)
        {
            TrapPlacing.SetActive(true);

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (!RaycastWithoutTriggers(ray, out hit))
                {
                    return;
                }
                focusObjs = Instantiate(_trap, hit.point, _trap.transform.rotation);
                DisableColliders();
            }
            else if (Input.GetMouseButton(0) && focusObjs != null)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (!RaycastWithoutTriggers(ray, out hit))
                {
                    return;
                }
                focusObjs.transform.position = hit.point;
            }
            else if (Input.GetMouseButtonUp(0) && focusObjs != null)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (!RaycastWithoutTriggers(ray, out hit))
                {
                    return;
                }
                if (hit.collider.gameObject.CompareTag("Tower_Place") && hit.normal.Equals(new Vector3(0, 1, 0)))
                {
                    hit.collider.gameObject.tag = "occupied";
                    focusObjs.transform.position = new Vector3(hit.collider.gameObject.transform.position.x, focusObjs.transform.position.y, hit.collider.gameObject.transform.position.z);
                    EnabledColliders();
                    money.payOrNot = true;
                }
                else
                {
                    Destroy(focusObjs);
                }
                focusObjs = null;
                holdingTrap = false;
                TrapPlacing.SetActive(false);
            }
        }
    }
    //-------------------------placing Tower
    public void PlaceTower(GameObject tower)
    {
        money.tradeOpen = true;
        money.tradeOpen2 = false;
        holdingTrap = false;
        TrapPlacing.SetActive(false);
        _tower = tower;
        _trap = null;
    }

    public void PlaceTrap(GameObject trap)
    {
        money.tradeOpen2 = true;
        money.tradeOpen = false;
        holdingTower = false;
        TowerPlacing.SetActive(false);
        _trap = trap;
        _tower = null;
    }

    private void DisableColliders()
    {
        SetCollidersEnabled(false);
    }

    private void EnabledColliders()
    {
        SetCollidersEnabled(true);
    }

    private void SetCollidersEnabled(bool enabled)
    {
        Collider[] childColliders = focusObjs.GetComponentsInChildren<Collider>(true);
        Collider[] mainColliders = focusObjs.GetComponents<Collider>();

        foreach(Collider collider in childColliders)
        {
            collider.enabled = enabled;
        }

        foreach (Collider collider in mainColliders)
        {
            collider.enabled = enabled;
        }
    }

    private bool RaycastWithoutTriggers(Ray ray, out RaycastHit hit)
    {
        RaycastHit[] hits = Physics.RaycastAll(ray);

        Array.Sort(hits, (x, y) => x.distance.CompareTo(y.distance));

        foreach (RaycastHit raycastHit in hits)
        {
            if (!raycastHit.collider.isTrigger)
            {
                hit = raycastHit;
                return true;
            }
        }

        hit = new RaycastHit();
        return false;
    }
}