using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class C6S1PlayerController : MonoBehaviour
{
    public C6S1GameManager manager;
    public float speed = 10f, bulletCooldownTime = 2f;
    public Transform spawnLocationTr, bulletParentTr;
    public GameObject bulletPrefab;
    public Image coolDownBackFill;
    public TMP_Text cooldownTxt;
    public Button fireButton, leftButton, rightButton;

    private float screenLeftBound;
    private float screenRightBound;
    private float playerWidth;
    private float cooldownTime = 0.0f;
    private bool bulletShot = false;
    private bool isMovingLeft = false, isMovingRight = false, fireButtonClicked = false;

    private void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            leftButton.gameObject.SetActive(true);
            rightButton.gameObject.SetActive(true);
            fireButton.gameObject.SetActive(true);
        }

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

            if (!bulletShot && (Input.GetKeyDown(KeyCode.Space) || fireButtonClicked))
            {
                Debug.Log("Fired" + fireButtonClicked);
                bulletShot = true;
                fireButtonClicked = false;
                GameObject bulletObj = Instantiate(bulletPrefab, spawnLocationTr.position, Quaternion.identity, bulletParentTr);

                bulletObj.GetComponent<C6S1BulletHandler>().OnHitOption.AddListener(manager.CheckCorrectAnswer);
            }
        }

        if(bulletShot)
        {
            cooldownTime += Time.deltaTime;
            cooldownTxt.color = Color.white;
            coolDownBackFill.color = Color.red;
            coolDownBackFill.fillAmount = cooldownTime / bulletCooldownTime;
            cooldownTxt.text = cooldownTime.ToString("F1");
            if(cooldownTime >= bulletCooldownTime)
            {
                fireButton.interactable = true;
                coolDownBackFill.fillAmount = 1;
                cooldownTxt.color = Color.black;
                coolDownBackFill.color = Color.green;
                cooldownTxt.text = "Hit";
                cooldownTime = 0.0f;
                bulletShot = false;
            }
        }

        if(isMovingLeft)
        {
            float newX = transform.position.x + (-speed * Time.deltaTime);

            // Clamp player position within bounds
            newX = Mathf.Clamp(newX, screenLeftBound + playerWidth, screenRightBound - playerWidth);

            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }

        if (isMovingRight)
        {
            float newX = transform.position.x + (speed * Time.deltaTime);

            // Clamp player position within bounds
            newX = Mathf.Clamp(newX, screenLeftBound + playerWidth, screenRightBound - playerWidth);

            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
    }

    public void MoveLeftPointerDown()
    {
        isMovingRight = false;
        isMovingLeft = true;
    }

    public void MoveLeftPointerUp()
    {
        isMovingLeft = false;
    }

    public void MoveRightPointerDown()
    {
        isMovingLeft = false;
        isMovingRight = true;
    }

    public void MoveRightPointerUp()
    {
        isMovingRight = false;
    }

    public void FireButtonAction()
    {
        Debug.Log("Fire Button Clicked");
        fireButtonClicked = true;
        fireButton.interactable = false;
    }
}
