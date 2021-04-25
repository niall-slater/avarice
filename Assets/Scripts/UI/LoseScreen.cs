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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        UIEventHub.Instance.RaiseOnSceneReload();
    }

    public void GiveUp()
    {
        SceneManager.LoadScene(0);
    }
}
