using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TopBarUpdater : MonoBehaviour
{
    public TextMeshProUGUI DepthReadout;
    public TextMeshProUGUI MonsterReadout;
    public TextMeshProUGUI CaravanReadout;
    public TextMeshProUGUI CashReadout;

    void Update()
    {
        int minutes = Mathf.FloorToInt(GameController.CaravanTimer / 60F);
        int seconds = Mathf.FloorToInt(GameController.CaravanTimer - minutes * 60);
        string formattedTime = string.Format("{0:0}:{1:00}", minutes, seconds);

        CaravanReadout.text = $"CARAVAN IN: {formattedTime}";
        MonsterReadout.text = $"{GameController.MonsterCount} ABERRATIONS";
        CashReadout.text = $"WEALTH EARNED:\n${Mathf.RoundToInt(GameController.Cash)}";
        DepthReadout.text = $"DEEPEST DRILL:\n{Mathf.RoundToInt(GameController.MaxDepthReached)}m";
    }
}
