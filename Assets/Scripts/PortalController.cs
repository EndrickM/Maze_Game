using UnityEngine;

public class PortalController : MonoBehaviour
{
    public Animator animator;
    public float interactionDistance = 1f;

    private Transform player;
    private bool playerInRange = false;
    private bool isOpen = false;
    private bool isClosing = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null || isClosing) return;

        float dist = Vector2.Distance(transform.position, player.position);
        bool isNear = dist <= interactionDistance;

        if (isNear && !playerInRange && !isOpen)
        {
            Debug.Log(playerInRange);
            playerInRange = true;
            Debug.Log(playerInRange);
            animator.SetTrigger("Open");
            Debug.Log(isOpen);
            isOpen = true;
            Debug.Log("Portal Aberto");
        }
        else if (!isNear && playerInRange)
        {
            Debug.Log(playerInRange);
            playerInRange = false;
            Debug.Log(playerInRange);
            StartClosing();
        }
    }

    void StartClosing()
    {
        if (isClosing) return;

        Debug.Log(isClosing);
        isClosing = true;
        Debug.Log(isClosing);
        animator.SetTrigger("Close");
        Debug.Log("Portal Fechando");
    }

    // Chame esse método ao final da animação Close com um Animation Event
    public void OnPortalClosed()
    {
        isOpen = false;
        isClosing = false;
        Debug.Log("Portal Fechado");
    }
}
