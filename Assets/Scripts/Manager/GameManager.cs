using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

using PlayerHealthInt = System.Int16;

using CustomExtensions;
public class GameManager : MonoBehaviour, IManager
{
    public int MaxItems = 4;

    public TMP_Text HealthText;
    public TMP_Text ItemsText;
    public TMP_Text ProgressText;

    public Button WinButton;

    public Button LoseButton;

    private int _itemsCollected = 0;
    private PlayerHealthInt _playerHP = 10;

    private string _state;

    public string State
    {
        get => _state;
        set => _state = value;
    }

    void Start()
    {
        Initialize();

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
                Debug.Log("You win!");
                WinButton.gameObject.SetActive(true);

                UpdateScene("You found all the items! You win!");
            }
            else
            {
                ProgressText.text = "Item found, only " + (MaxItems - Items) + " more!";
            }
        }
    }

    public PlayerHealthInt HP
    {
        get { return _playerHP; }
        set
        {
            _playerHP = value;
            Debug.Log("Player HP: " + _playerHP);
            HealthText.text = "HP: " + _playerHP;

            if (_playerHP <= 0)
            {
                Debug.Log("You lose!");
                LoseButton.gameObject.SetActive(true);

                UpdateScene("You have been defeated! Game Over!");

            }
            else
            {
                ProgressText.text = "Ouch! That's got to hurt! HP: " + _playerHP;
            }
        }
    }

    public void RestartGame()
    {
        Utilities.RestartLevel();
    }

    public void UpdateScene(string UpdateText)
    {
        ProgressText.text = UpdateText;
        Time.timeScale = 0;
    }

    public void Initialize()
    {
        _state = "GameManager initialized";
        _state.FancyDebug();
        Debug.Log(_state);
    }
}
