using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreen : MonoBehaviour
{
    public TextMeshProUGUI Reason;

    public void Retry()
    {
        UIEventHub.Instance.RaiseOnSceneReload();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GiveUp()
    {
        UIEventHub.Instance.RaiseOnSceneReload();
        SceneManager.LoadScene(0);
    }
}
