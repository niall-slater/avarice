using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreen : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        UIEventHub.Instance.RaiseOnSceneReload();
    }

    public void GiveUp()
    {
        SceneManager.LoadScene(0);
    }
}
