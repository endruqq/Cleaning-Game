using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class Dirt : MonoBehaviour
{
    public TMP_Text moneyTextTMP; // Assign in inspector or via tag
    public float moneyReward = 2.5f;

    public int dirtId;
    public float cleanTime = 3.0f;
    public Slider progressBar; // Optional, unused in shared system
    public GameObject indicatorCanvas; // Set this as child of each Dirt prefab in Inspector

    private Slider sharedProgressBar;
    private float currentCleanProgress = 0f;
    private bool isCleaning = false;
    private PlayerInteraction player;
    private Coroutine cleaningCoroutine;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (indicatorCanvas != null)
            indicatorCanvas.SetActive(false);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        TryFindUIReferences();

        if (progressBar != null)
            progressBar.gameObject.SetActive(false);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(DelayedReferenceRefresh());
    }

    private IEnumerator DelayedReferenceRefresh()
    {
        yield return new WaitForSeconds(0.1f);
        TryFindUIReferences();
    }

    GameObject FindInactiveObjectByTag(string tag)
    {
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.CompareTag(tag) && !obj.activeInHierarchy)
                return obj;
        }

        return null;
    }

    private void TryFindUIReferences()
    {
        if (moneyTextTMP == null)
        {
            GameObject moneyGO = GameObject.FindWithTag("MoneyText");
            if (moneyGO != null)
                moneyTextTMP = moneyGO.GetComponent<TMP_Text>();
        }

        if (progressBar == null)
        {
            GameObject progressGO = FindInactiveObjectByTag("CleaningProgressBar");
            if (progressGO != null)
            {
                progressBar = progressGO.GetComponent<Slider>();
                Debug.Log("Found progressBar even though it's inactive.");
            }
            else
            {
                Debug.LogWarning("progressBar not found, even with inactive search.");
            }
        }
    }

    public void ShowIndicator(bool show)
    {
        if (transform.childCount > 0)
        {
            Transform indicator = transform.GetChild(0);
            indicator.gameObject.SetActive(show);
        }
    }


    public bool IsCleaning()
    {
        return isCleaning;
    }

    public void StartCleaning(PlayerInteraction playerInteraction, Slider progressBar)
    {
        if (isCleaning) return;

        isCleaning = true;
        player = playerInteraction;
        sharedProgressBar = progressBar;

        if (sharedProgressBar != null)
        {
            sharedProgressBar.gameObject.SetActive(true);
            sharedProgressBar.value = 0f;
        }

        cleaningCoroutine = StartCoroutine(CleanRoutine());
    }

    private IEnumerator CleanRoutine()
    {
        while (currentCleanProgress < cleanTime)
        {
            currentCleanProgress += Time.deltaTime;
            float progressValue = currentCleanProgress / cleanTime;

            if (sharedProgressBar != null)
                sharedProgressBar.value = progressValue;

            yield return null;
        }

        CompleteCleaning();
    }

    public void StopCleaning()
    {
        if (!isCleaning) return;

        isCleaning = false;

        if (cleaningCoroutine != null)
        {
            StopCoroutine(cleaningCoroutine);
            cleaningCoroutine = null;
        }

        currentCleanProgress = 0f;

        if (sharedProgressBar != null)
        {
            sharedProgressBar.value = 0f;
            sharedProgressBar.gameObject.SetActive(false);
        }
    }

    void CompleteCleaning()
    {
        isCleaning = false;

        if (progressBar != null)
        {
            progressBar.value = 1f;
            progressBar.gameObject.SetActive(false);
        }

        if (player != null)
        {
            player.StopCleaning();
        }

        if (moneyTextTMP != null)
        {
            float current = float.Parse(moneyTextTMP.text.Replace("$", "").Trim());
            moneyTextTMP.text = $"${current + moneyReward:F2}";
        }

        Destroy(gameObject);
    }
}
