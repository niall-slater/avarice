using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarracksMenu : MonoBehaviour
{
    public Barracks SelectedBarracks;

    public void TrainMarine()
    {
        if (GameController.Cash < SelectedBarracks.MarineCost)
        {
            Debug.Log("Can't afford a marine");
            return;
        }

        GameController.SpawnMarine(SelectedBarracks.transform.position);
        GameController.Cash -= SelectedBarracks.MarineCost;
    }
}
