using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BarracksMenu : MonoBehaviour
{
    public Barracks SelectedBarracks;

    public TextMeshProUGUI MarineText;
    public TextMeshProUGUI BuilderText;
    public TextMeshProUGUI APCText;

    private void OnEnable()
    {
        MarineText.text = $"${Barracks.MarineCost}\nCALL MARINE";
        BuilderText.text = $"${Barracks.BuilderCost}\nCALL BUILDER";
        APCText.text = $"${Barracks.APCCost}\nCALL APC";
    }

    public void TrainMarine()
    {
        if (GameController.Cash < Barracks.MarineCost)
        {
            Debug.Log("Can't afford a marine");
            return;
        }

        GameController.SpawnMarine(SelectedBarracks.transform.position);
        GameController.Cash -= Barracks.MarineCost;
    }

    public void TrainBuilder()
    {
        if (GameController.Cash < Barracks.BuilderCost)
        {
            Debug.Log("Can't afford a builder");
            return;
        }

        GameController.SpawnBuilder(SelectedBarracks.transform.position);
        GameController.Cash -= Barracks.BuilderCost;
    }

    public void TrainAPC()
    {
        if (GameController.Cash < Barracks.APCCost)
        {
            Debug.Log("Can't afford an APC");
            return;
        }

        GameController.SpawnAPC(SelectedBarracks.transform.position);
        GameController.Cash -= Barracks.APCCost;
    }
}
