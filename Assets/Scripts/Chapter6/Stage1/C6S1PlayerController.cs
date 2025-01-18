using DG.Tweening;
using UnityEngine;

public class C6S1PlayerController : MonoBehaviour
{
    public C6S1GameManager manager;
    public float speed = 10f;
    public Transform spawnLocationTr, bulletParentTr;
    public GameObject bulletPrefab;

    private float screenLeftBound;
    private float screenRightBound;
    private float playerWidth;

    private void Start()
    {
        // Get screen bounds in world coordinates
        screenLeftBound = Camera.main.ScreenToWorldPoint(Vector3.zero).x;
        screenRightBound = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;

        // Get player width (assuming a SpriteRenderer)
        playerWidth = GetComponent<SpriteRenderer>().bounds.extents.x; // Half-width of player
    }

    private void Update()
    {
        if (!manager.pauseGame)
        {
            float horizontalMovement = Input.GetAxis("Horizontal");
            float newX = transform.position.x + (horizontalMovement * speed * Time.deltaTime);

            // Clamp player position within bounds
            newX = Mathf.Clamp(newX, screenLeftBound + playerWidth, screenRightBound - playerWidth);

            transform.position = new Vector3(newX, transform.position.y, transform.position.z);

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
            {
                GameObject bulletObj = Instantiate(bulletPrefab, spawnLocationTr.position, Quaternion.identity, bulletParentTr);

                bulletObj.GetComponent<C6S1BulletHandler>().OnHitOption.AddListener(manager.CheckCorrectAnswer);
            }
        }
    }
}
