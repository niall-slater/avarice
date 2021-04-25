using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DetailWindow : MonoBehaviour
{
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI TypeText;
    public TextMeshProUGUI HPText;
    public TextMeshProUGUI DetailsText;

    void Awake()
    {
        UIEventHub.Instance.OnSelectionChanged += HandleSelectionChanged;
    }

    private void HandleSelectionChanged(List<Actor> selected)
    {
        if (selected.Count != 1)
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);

        UpdateInfo(selected[0]);
    }

    private void UpdateInfo(Actor actor)
    {
        if (actor is Marine || actor is Monster || actor is Caravan)
            NameText.text = actor.ActorName;
        else
            NameText.text = string.Empty;

        TypeText.text = actor.GetType().ToString();
        HPText.text = $"HP: {actor.HP}";

        var details = string.Empty;

        if (actor is BioBomb b)
        {
            details = $"{b.SecondsLeft}s to detonation";
        }
        else if (actor is Caravan c)
        {
            details = $"{c.CollectedOre}T ore collected";
        }
        else if (actor is Gate g)
        {
            details = g.Open ? "open" : "closed";
        }
        else if (actor is Marine ma)
        {
            details = $"{ma.KillCount} confirmed kills";
        }
        else if (actor is Monster mo)
        {
            details = $"{mo.KillCount} kills";
        }
        else if (actor is Mine mi)
        {
            details = $"drill is {mi.MiningDepth}m deep";
        }
        DetailsText.text = details;
    }

    private void OnDestroy()
    {
        UIEventHub.Instance.OnSelectionChanged -= HandleSelectionChanged;
    }
}
