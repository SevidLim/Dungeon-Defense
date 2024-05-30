using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeState : MonoBehaviour
{
    [Header("Other Script")]
    public Tower tower;
    
    public Money money;
    public GameObject _money;

    [Header("Sphere Collider")]
    public SphereCollider attackRange;
    public GameObject ShowAttackRange;

    [Header("price Text")]
    public Text _damage;
    public Text _speed;
    public Text _range;

    private int D_price = 5;
    private int S_price = 5;
    private int R_price = 5;

    [Header("sell Text")]
    public int totalspend;
    public Text sellText;

    [Header("state Text")]
    public Text D_State;
    public Text S_State;
    public Text R_State;

    [Header("Upgrade icon")]
    public GameObject[] lv_d;
    public GameObject[] lv_s;
    public GameObject[] lv_r;

    private int damage = -1;
    private int speed = -1;
    private int range = -1;

    void Start()
    {
        _money = GameObject.Find("Money");
        if(_money != null)
        {
            money = _money.GetComponent<Money>();
        }

        _damage.text = D_price.ToString() + "$";
        _speed.text = S_price.ToString() + "$";
        _range.text = R_price.ToString() + "$";
    }

    void Update()
    {
        D_State.text = tower._damage.ToString();

        S_State.text = tower.secondsLeft.ToString();

        R_State.text = ShowAttackRange.transform.localScale.x.ToString();
        ShowAttackRange.transform.localScale = Vector3.one * (attackRange.radius * 2f);

        sellText.text = totalspend.ToString() + "$";
    }

    public void DamageUpgrade()
    {
        if (damage > 4)
        {
            tower._damage += 0;
        }
        else if (damage < 4)
        {
            money._payment = D_price;
            money.Upgrade = true;

            if (money._money >= money._payment)
            {
                damage += 1;
                tower._damage += 1;
                totalspend += D_price / 2;
                lv_d[damage].SetActive(true);
                money._paymentHide = D_price;
            }

            if (damage == 0)
            {
                D_price = 10;
            }
            else if (damage == 1)
            {
                D_price = 15;
            }
            else if (damage == 2)
            {
                D_price = 20;
            }
            else if (damage == 3)
            {
                D_price = 25;
            }

            _damage.text = D_price.ToString() + "$";

            if (damage == 4)
            {
                _damage.text = "Lv Max";
            }
        }
    }

    public void SpeedUpgrade()
    {
        if (speed > 4)
        {
            tower.secondsLeft -= 0;
        }
        else if(speed < 4)
        {
            money._payment = S_price;
            money.Upgrade = true;

            if (money._money >= money._payment)
            {
                speed += 1;
                tower.secondsLeft -= 0.1f;
                totalspend += S_price / 2;
                lv_s[speed].SetActive(true);
                money._paymentHide = S_price;
            }

            if (speed == 0)
            {
                S_price = 10;
            }
            else if(speed == 1)
            {
                S_price = 15;
            }
            else if (speed == 2)
            {
                S_price = 20;
            }
            else if (speed == 3)
            {
                S_price = 25;
            }

            _speed.text = S_price.ToString() + "$";

            if (speed == 4)
            {
                _speed.text = "Lv Max";
            }
        }
    }

    public void RangeUpgrade()
    {
        if (range > 4)
        {
            attackRange.radius += 0;
        }
        else if (range < 4)
        {
            money._payment = R_price;
            money.Upgrade = true;

            if (money._money >= money._payment)
            {
                range += 1;
                attackRange.radius += 0.5f;
                totalspend += R_price / 2;
                lv_r[range].SetActive(true);
                money._paymentHide = R_price;
            }

            if (range == 0)
            {
                R_price = 10;
            }
            else if (range == 1)
            {
                R_price = 15;
            }
            else if (range == 2)
            {
                R_price = 20;
            }
            else if (range == 3)
            {
                R_price = 25;
            }

            _range.text = R_price.ToString() + "$";

            if (range == 4)
            {
                _range.text = "Lv Max";
            }
        }
    }
}