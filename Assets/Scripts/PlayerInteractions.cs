using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 1f;
    public LayerMask dirtLayer;
    public LayerMask repairLayer;
    public Animator animator;
    public Slider sharedProgressBar;

    private Dirt currentDirt;
    private Repair currentRepair;
    private GameObject interactionIndicatorForRepair;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        interactionIndicatorForRepair = GameObject.FindWithTag("IndicatorForRepair");
    }

    void Update()
    {
        if (interactionIndicatorForRepair == null)
        {
            interactionIndicatorForRepair = GameObject.FindWithTag("IndicatorForRepair");
        }

        HandleDirtDetection();
        HandleRepairDetection();
    }

    void HandleDirtDetection()
    {
        Collider2D[] dirtColliders = Physics2D.OverlapCircleAll(transform.position, interactionRange, dirtLayer);

        Dirt closestDirt = null;
        float closestDistance = float.MaxValue;

        foreach (var col in dirtColliders)
        {
            Dirt dirt = col.GetComponent<Dirt>();
            if (dirt != null)
            {
                float dist = Vector2.Distance(transform.position, dirt.transform.position);
                if (dist < closestDistance)
                {
                    closestDistance = dist;
                    closestDirt = dirt;
                }

                // Hide all indicators
                dirt.ShowIndicator(false);
            }
        }

        currentDirt = closestDirt;

        bool hasCorrectItems = CheckForRequiredCombination();

        if (currentDirt != null && hasCorrectItems)
        {
            currentDirt.ShowIndicator(true);

            if (Input.GetKey(KeyCode.E))
                StartCleaning();
            else
                StopCleaning();
        }
        else
        {
            StopCleaning();
        }
    }

    void HandleRepairDetection()
    {
        Collider2D repairCollider = Physics2D.OverlapCircle(transform.position, interactionRange, repairLayer);

        if (repairCollider)
        {
            currentRepair = repairCollider.GetComponent<Repair>();

            if (interactionIndicatorForRepair != null)
            {
                interactionIndicatorForRepair.SetActive(true);
            }

            if (Input.GetKey(KeyCode.E))
                StartRepairing();
            else
                StopRepairing();
        }
        else
        {
            if (interactionIndicatorForRepair != null)
            {
                interactionIndicatorForRepair.SetActive(false);
            }

            StopRepairing();
            currentRepair = null;
        }
    }

    bool CheckForRequiredCombination()
    {
        if (currentDirt == null) return false;

        bool hasItem1 = InventorySlotForToolBar.slots.TryGetValue(1, out var slot1) &&
                        slot1.currentItem != null &&
                        slot1.currentItem.id == 1;

        if (currentDirt.dirtId == 1)
        {
            bool hasItem2 = InventorySlotForToolBar.slots.TryGetValue(3, out var slot2) &&
                            slot2.currentItem != null &&
                            slot2.currentItem.id == 2;

            return hasItem1 && hasItem2;
        }
        else if (currentDirt.dirtId == 2)
        {
            bool hasItem2 = InventorySlotForToolBar.slots.TryGetValue(3, out var slot2) &&
                            slot2.currentItem != null &&
                            slot2.currentItem.id == 8;

            return hasItem1 && hasItem2;
        }

        return false;
    }

    public void StartCleaning()
    {
        if (currentDirt != null && !currentDirt.IsCleaning())
        {
            animator.SetBool("IsCleaning", true);
            currentDirt.StartCleaning(this, sharedProgressBar);
        }
    }

    public void StopCleaning()
    {
        animator.SetBool("IsCleaning", false);
        if (currentDirt != null)
        {
            currentDirt.StopCleaning();
        }
    }

    public void StartRepairing()
    {
        if (currentRepair != null && !currentRepair.IsRepairing())
        {
            animator.SetBool("isRepairing", true);
            currentRepair.StartRepairing(this);
        }
    }

    public void StopRepairing()
    {
        animator.SetBool("isRepairing", false);
        if (currentRepair != null)
        {
            currentRepair.StopRepairing();
        }
    }
}
