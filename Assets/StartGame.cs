using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    [SerializeField]
    Canvas loadingPanel;

    [SerializeField]
    Image loadingBar;

    bool isLoading;

    float fakeLoadingProgress;

    float timeWhenStartedLoading;
    float timeSinceLoading;
    [SerializeField]
    private float timeIncreaseMultiplier = 1;

    float actualProgress;

    void Start()
    {
        loadingPanel.enabled = false;;
    }

    void FixedUpdateUpdate()
    {   

        if(isLoading)
        {
            loadingBar.fillAmount = fakeLoadingProgress;
            timeSinceLoading = Time.time - timeWhenStartedLoading;
            fakeLoadingProgress = timeSinceLoading + (Time.deltaTime + timeIncreaseMultiplier) + actualProgress;
            loadingBar.fillAmount = fakeLoadingProgress;
        }
    }

    // Function to load Scene 2 asynchronously with a progress bar
    public void LoadTHEGAMEAsync()
    {
        StartCoroutine(LoadSceneAsync("THEACTUALGAME"));
    }

    // Coroutine to handle asynchronous scene loading
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        // Optional: Show a loading screen or progress UI here
        loadingPanel.enabled = true;
        isLoading = true;
        timeWhenStartedLoading = Time.time;

        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            Debug.Log($"Loading progress: {progress * 100}%");
            actualProgress =  progress;
            // Update the progress bar or loading UI (if applicable)

            yield return null;
        }

        // Optional: Hide the loading screen once done
        SceneManager.LoadScene(sceneName);
    }
}
