using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartupController : MonoBehaviour
{
    [SerializeField] private Slider progressBar;

    void Awake()
    {
        Messenger<int, int>.AddListener(StartupEvent.MANAGERS_PROGRESS, OnManagerProgress);
        Messenger.AddListener(StartupEvent.MANAGERS_STARTED, OnManagerStarted);
    }

    void OnDestroy() {
        Messenger<int, int>.RemoveListener(StartupEvent.MANAGERS_PROGRESS, OnManagerProgress);
        Messenger.RemoveListener(StartupEvent.MANAGERS_STARTED, OnManagerStarted);
    }
    
    public void OnManagerProgress(int numReady, int numModules) {
        float progress = (float)numReady / numModules;
        progressBar.value = progress;
    }

    public void OnManagerStarted() {
        Managers.Mission.GoToNext();
    }
}
