using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public void HaveMercy()
    {
        UIEventHub.Instance.RaiseOnSceneReload();
        SceneManager.LoadScene(0);
    }

    public void DrillDeeper()
    {
        var nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        try
        {
            SceneManager.LoadScene(nextIndex);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            SceneManager.LoadScene(0);
        }
    }
}
