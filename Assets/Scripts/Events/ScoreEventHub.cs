using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreEventHub
{
    private static ScoreEventHub _instance;

    public static ScoreEventHub Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ScoreEventHub();
                UIEventHub.Instance.OnSceneReload += Destroy;
            }
            return _instance;
        }
    }

    public static void Destroy() { _instance = null; }

    /// <summary>
    /// The maximum depth reached is updated.
    /// </summary>
    public delegate void DepthUpdated(float newDepth); public event DepthUpdated OnDepthUpdated;
    public void RaiseOnDepthUpdated(float newDepth) { OnDepthUpdated?.Invoke(newDepth); }

    /// <summary>
    /// The caravan timer is updated.
    /// </summary>
    public delegate void CaravanTimerUpdated(float newCaravanTimer); public event CaravanTimerUpdated OnCaravanTimerUpdated;
    public void RaiseOnCaravanTimerUpdated(float newCaravanTimer) { OnCaravanTimerUpdated?.Invoke(newCaravanTimer); }

    /// <summary>
    /// The cash amount is updated.
    /// </summary>
    public delegate void CashAmountUpdated(float cashChange); public event CashAmountUpdated OnCashAmountUpdated;
    public void RaiseOnCashAmountUpdated(float cashChange) { OnCashAmountUpdated?.Invoke(cashChange); }

    /// <summary>
    /// The bio bomb detonates.
    /// </summary>
    public delegate void BioBombDetonation(); public event BioBombDetonation OnBioBombDetonation;
    public void RaiseOnBioBombDetonation() { OnBioBombDetonation?.Invoke(); }

    /// <summary>
    /// The bio bomb is built.
    /// </summary>
    public delegate void BioBombBuilt(BioBomb bomb); public event BioBombBuilt OnBioBombBuilt;
    public void RaiseOnBioBombBuilt(BioBomb bomb) { OnBioBombBuilt?.Invoke(bomb); }
}
