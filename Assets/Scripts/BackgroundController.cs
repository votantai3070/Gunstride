using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BackgroundController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform cameraTarget;

    [Header("Parallax")]
    [Range(0f, 1f)]
    [SerializeField] private float parallaxFactor = 0.5f;

    [Header("Loop")]
    [SerializeField] private bool loopHorizontally = true;

    private SpriteRenderer spriteRenderer;
    private float spriteWidth;
    private float startX;
    private float previousCameraX;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (cameraTarget == null && Camera.main != null)
            cameraTarget = Camera.main.transform;
    }

    private void Start()
    {
        startX = transform.position.x;
        previousCameraX = cameraTarget != null ? cameraTarget.position.x : 0f;
        spriteWidth = spriteRenderer.bounds.size.x;
    }

    private void FixedUpdate()
    {
        if (cameraTarget == null || spriteWidth <= 0f)
            return;

        float cameraDeltaX = cameraTarget.position.x - previousCameraX;

        Vector3 position = transform.position;
        position.x += cameraDeltaX * parallaxFactor;
        transform.position = position;

        if (loopHorizontally)
            HandleLoop();

        previousCameraX = cameraTarget.position.x;
    }

    private void HandleLoop()
    {
        float distanceToCamera = cameraTarget.position.x - transform.position.x;

        if (distanceToCamera > spriteWidth)
        {
            transform.position += Vector3.right * spriteWidth;
            startX = transform.position.x;
        }
        else if (distanceToCamera < -spriteWidth)
        {
            transform.position += Vector3.left * spriteWidth;
            startX = transform.position.x;
        }
    }

    private void OnValidate()
    {
        parallaxFactor = Mathf.Clamp01(parallaxFactor);
    }
}