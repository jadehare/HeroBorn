using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Utilities
{
    public static int PlayerDeaths = 0;

    public static string UpdateDeathCount(ref int countRef)
    {
        countRef += 1;
        return "Next time you'll be at number " + countRef + "!";
    }

    public static void RestartLevel()
    {
        string message = UpdateDeathCount(ref PlayerDeaths);
        Debug.Log("Player deaths " + PlayerDeaths);
        Debug.Log(message);

        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}