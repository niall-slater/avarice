using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public void HaveMercy()
    {
        SceneManager.LoadScene(0);
    }

    public void DrillDeeper()
    {
        var nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (SceneManager.GetSceneByBuildIndex(nextIndex) != null)
        {
            SceneManager.LoadScene(nextIndex);
            UIEventHub.Instance.RaiseOnSceneReload();
        }
        else
        {
            Debug.Log("No more scenes");
            SceneManager.LoadScene(0);
        }
    }
}
