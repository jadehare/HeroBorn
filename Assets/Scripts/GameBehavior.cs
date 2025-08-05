using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int MaxItems = 4;

    public TMP_Text HealthText;
    public TMP_Text ItemsText;
    public TMP_Text ProgressText;

    public Button WinButton;


    private int _itemsCollected = 0;
    private int _playerHP = 10;

    void Start()
    {
        HealthText.text += _playerHP;
        ItemsText.text += _itemsCollected;
    }


    public int Items
    {
        get { return _itemsCollected; }
        set
        {
            _itemsCollected = value;
            Debug.Log("Items collected: " + _itemsCollected);

            ItemsText.text = "Items: " + Items;

            if (Items >= MaxItems)
            {
                ProgressText.text = "You win!";
                Debug.Log("You win!");
                WinButton.gameObject.SetActive(true);

                Time.timeScale = 0;
            }
            else
            {
                ProgressText.text = "Item found, only " + (MaxItems - Items) + " more!";
            }
        }
    }

    public int HP
    {
        get { return _playerHP; }
        set
        {
            _playerHP = value;
            Debug.Log("Player HP: " + _playerHP);
            HealthText.text = "HP: " + _playerHP;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
