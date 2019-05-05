using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartupController : MonoBehaviour
{
    [SerializeField] private Slider progressBar;

    private void Awake()
    {
        Messenger<int, int>.AddListener(StartupEvent.MANAGES_PROGRESS, OnManagerProgress);
        Messenger.AddListener(StartupEvent.MANAGES_STARTED, OnManagerStarted);
    }

    private void OnDestroy()
    {
        Messenger<int, int>.RemoveListener(StartupEvent.MANAGES_PROGRESS, OnManagerProgress);
        Messenger.RemoveListener(StartupEvent.MANAGES_STARTED, OnManagerStarted);
    }

    public void OnManagerProgress(int numReady, int numModules)
    {
        float progress = (float)numReady / numModules;
        progressBar.value = progress;
    }

    public void OnManagerStarted()
    {
        Managers.Mission.GoToNext();
    }
}
