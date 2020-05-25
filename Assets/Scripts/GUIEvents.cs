using System;
using UnityEngine;

//Events
public class GUIEvents : MonoBehaviour
{
    public static GUIEvents guiEvents;

    private void Awake()
    {
        if(guiEvents != null)
        {
            GameObject.Destroy(guiEvents);
        }
        else
        {
            guiEvents = this;
        }
    }

    public event Action<int> onScoreTrigger;
    public void OnScore(int Amount)
    {
        if (onScoreTrigger != null)
        {
            onScoreTrigger(Amount);
        }
    }

    public event Action onLiveDeplateTrigger;
    public void onLiveDeplate()
    {
        if (onLiveDeplateTrigger != null)
        {
            onLiveDeplateTrigger();
        }
    }

    public event Action onWaveIncrementTrigger;
    public void onWaveIncrement()
    {
        if (onWaveIncrementTrigger != null)
        {
            onWaveIncrementTrigger();
        }
    }
}
