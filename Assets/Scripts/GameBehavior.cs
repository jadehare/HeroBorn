using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int _itemsCollected = 0;
    private int _playerHP = 10;


    public int Items
    {
        get { return _itemsCollected; }
        set
        {
            _itemsCollected = value;
            Debug.Log("Items collected: " + _itemsCollected);
        }
    }

    public int HP
    {
        get { return _playerHP; }
        set
        {
            _playerHP = value;
            Debug.Log("Player HP: " + _playerHP);
        }
    }
}
