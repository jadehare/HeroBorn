using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

using PlayerHealthInt = System.Int16;

using CustomExtensions;
using System.Linq;
public class GameManager : MonoBehaviour, IManager
{
    public int MaxItems = 4;

    public TMP_Text HealthText;
    public TMP_Text ItemsText;
    public TMP_Text ProgressText;

    public Button WinButton;

    public Button LoseButton;

    public delegate void DebugDelegate(string message);

    public DebugDelegate DebugEvent = Print;

    public static void Print(string message)
    {
        Debug.Log(message);
    }

    public PlayerBehavior playerBehavior;

    private int _itemsCollected = 0;
    private PlayerHealthInt _playerHP = 10;

    private string _state;

    public string State
    {
        get => _state;
        set => _state = value;
    }

    public Stack<Loot> LootStack = new Stack<Loot>();

    void Start()
    {
        Initialize();

        HealthText.text += _playerHP;
        ItemsText.text += _itemsCollected;

        LootStack.Push(new Loot("Sword of Doom", 5));
        LootStack.Push(new Loot("HP Boost", 1));
        LootStack.Push(new Loot("Golden Key", 3));
        LootStack.Push(new Loot("Pair of Winged Boots", 2));
        LootStack.Push(new Loot("Mythril Bracer", 4));

        FilterLoot();
    }

    void OnEnable()
    {
        GameObject player = GameObject.Find("Player");
        playerBehavior = player.GetComponent<PlayerBehavior>();
        playerBehavior.eventInstance += EventHandler;
    }

    void OnDisable()
    {
        playerBehavior.eventInstance -= EventHandler;
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

        DebugEvent(_state);
    }

    public void PrintLootReport()
    {
        Debug.LogFormat("There are {0} items in the loot stack.", LootStack.Count);
    }

    public void FilterLoot()
    {
        var rareLoot = LootStack.Where(item => item.Rarity >= 3).OrderBy(item => item.Rarity).Select(item => item.Name);
        foreach (var lootName in rareLoot)
        {
            Debug.LogFormat("Rare loot found: {0}", lootName);
        }
    }

    
    public void EventHandler(int type, string message)
    {
        switch (type)
        {
            case 0:
                Debug.Log("Event Type 0: " + message);
                break;
            case 1:
                Debug.Log("Event Type 1: " + message);
                break;
            default:
                Debug.Log("Unknown event type: " + type);
                break;
        }
    }

}
