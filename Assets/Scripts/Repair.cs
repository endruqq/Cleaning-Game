using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Repair : MonoBehaviour
{
    public float repairTime = 3.0f;
    public Slider progressBar; // Can assign in inspector or find by tag/name

    private float currentRepairProgress = 0f;
    private bool isRepairing = false;
    private PlayerInteraction player;
    private Coroutine repairCoroutine;
    public GameObject repairedObject;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        repairedObject.SetActive(false);

        TryFindProgressBar();

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
        TryFindProgressBar();
    }

    GameObject FindInactiveObjectByTag(string tag)
    {
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.CompareTag(tag) && !obj.activeInHierarchy)
            {
                return obj;
            }
        }

        return null;
    }

    private void TryFindProgressBar()
    {
        if (progressBar == null)
        {
            GameObject progressGO = FindInactiveObjectByTag("RepairProgressBar");
            if (progressGO != null)
            {
                progressBar = progressGO.GetComponent<Slider>();
            }
            else
            {
                Debug.LogWarning("Repair progress bar not found.");
            }
        }
    }

    public bool IsRepairing()
    {
        return isRepairing;
    }

    public void StartRepairing(PlayerInteraction playerInteraction)
    {
        if (isRepairing) return;

        isRepairing = true;
        player = playerInteraction;

        if (progressBar != null)
        {
            progressBar.gameObject.SetActive(true);
            progressBar.value = 0f;
        }

        repairCoroutine = StartCoroutine(RepairRoutine());
    }

    private IEnumerator RepairRoutine()
    {
        while (currentRepairProgress < repairTime)
        {
            currentRepairProgress += Time.deltaTime;
            float progressValue = currentRepairProgress / repairTime;

            if (progressBar != null)
            {
                progressBar.value = progressValue;
            }

            yield return null;
        }

        CompleteRepair();
    }

    public void StopRepairing()
    {
        if (!isRepairing) return;

        isRepairing = false;

        if (repairCoroutine != null)
        {
            StopCoroutine(repairCoroutine);
            repairCoroutine = null;
        }

        currentRepairProgress = 0f;

        if (progressBar != null)
        {
            progressBar.value = 0f;
            progressBar.gameObject.SetActive(false);
        }
    }

    void CompleteRepair()
    {
        isRepairing = false;

        if (progressBar != null)
        {
            progressBar.value = 1f;
            progressBar.gameObject.SetActive(false);
        }

        if (player != null)
        {
            player.StopRepairing();
        }

        gameObject.SetActive(false);
        repairedObject.SetActive(true);
    }


}
