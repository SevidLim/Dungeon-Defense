using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    [Header("player_Wallet")]
    public int _money;
    public bool testing;
    public Text _text;
    [Header("Pay to buy Tower")]
    public int _payment;
    public GameObject NotEnough;

    public int _paymentHide;

    [Header("With UIInterface")]
    public bool tradeOpen;
    public bool tradeOpen2;
    public bool payOrNot = false;
    public bool Upgrade = false;
    public UIInterface uIInterface;

    void Start()
    {
        if (testing)
        {
            _money = 99999;
        }
        else
        {
            _money = 10;
        }
    }

    void Update()
    {
        _text.text = _money.ToString();
        _payment = _paymentHide;

        if(_money >= 99999)
        {
            _money = 99999;
        }

        if (tradeOpen)
        {
            if (_money < _payment)
            {
                uIInterface.holdingTower = false;
                NotEnough.SetActive(true);
            }
            else if (_money >= _payment)
            {
                uIInterface.holdingTower = true;
            }
            tradeOpen = false;
        }

        if (tradeOpen2)
        {
            if (_money < _payment)
            {
                uIInterface.holdingTrap = false;
                NotEnough.SetActive(true);
            }
            else if (_money >= _payment)
            {
                uIInterface.holdingTrap = true;
            }
            tradeOpen2 = false;
        }

        if (payOrNot)
        {
            _money -= _payment;
            payOrNot = false;
        }

        if (Upgrade)
        {
            if (_money < _payment)
            {
                NotEnough.SetActive(true);
            }
            else if (_money >= _payment)
            {
                _money -= _payment;
            }
            Upgrade = false;
        }
    }

    public void BuyTower(int payment)
    {
        _paymentHide = payment;
    }
}
