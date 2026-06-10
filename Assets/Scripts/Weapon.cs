using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Weapon : MonoBehaviour
{
    public int damage;
    public Transform pivot;
    public Transform firePoint;
    public GameObject arrowPrefab;
    public GameObject arrowManager;
    public int arrowCount = 10;
    public RectTransform dragArea; // Reference to the UI Panel
    public float shootCooldown = 0.1f; // Cooldown time in seconds
    public float rotationSpeed = 1.0f; // Speed of rotation

    public GameObject arm;
    private bool canShoot = true;
    private Vector2 touchStartPos;
    private Vector2 firstTouchPos;
    private bool isAiming = false;
    public AudioSource stretchSound; // Reference to the AudioSource component
    public AudioSource arrowRelease;

    public UIManager uiManager;

    public int headshots = 0;
    public static GameObject game;

    void Start()
    {
        game = GameObject.Find("Game");
        if(game == null)
        {
            game = GameObject.Find("Game 1");
        }
        stretchSound = arm.GetComponentInChildren<AudioSource>();
        arrowManager = GameObject.Find("ArrowManager");
        dragArea = GameObject.Find("Panel").GetComponent<RectTransform>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        arrowRelease = arm.transform.Find("Bow/Arrow").GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Check if the touch is within the bounds of the drag area
            if (RectTransformUtility.RectangleContainsScreenPoint(dragArea, touch.position))
            {
                if (touch.phase == TouchPhase.Began)
                {
                    touchStartPos = touch.position;
                    firstTouchPos = touch.position;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    Vector2 touchEndPos = touch.position;
                    Vector2 delta = touchEndPos - touchStartPos;

                    if (!isAiming && (firstTouchPos - touchEndPos).y > 10)
                    {
                        isAiming = true;
                        PlayAimFeedback();
                    }

                    if (isAiming)
                    {
                        float rotationAmount = delta.x * rotationSpeed * Time.deltaTime;
                        arm.transform.RotateAround(pivot.position, Vector3.forward, rotationAmount);
                    }

                    touchStartPos = touchEndPos;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    if (isAiming)
                    {
                        Vector2 touchEndPos = touch.position;
                        if ((firstTouchPos - touchEndPos).y > -10)
                        {
                            isAiming = false;
                            if (arrowCount > 0 && canShoot)
                            {
                                Shoot();
                                PlayArrowRelease();
                                StartCoroutine(Cooldown());
                            }
                        }
                        else
                        {
                            isAiming = false;
                        }
                    }
                }
            }
        }
        GameObject temp = GameObject.FindWithTag("Projectiles");
        if(arrowCount == 0 && temp == null){
            StartCoroutine(WaitOneSecond());
            uiManager.ShowGameoverCanvas();
        }
    }
    private IEnumerator WaitOneSecond()
    {
        // Wait for one second
        yield return new WaitForSeconds(1.0f);
    }

    void PlayAimFeedback()
    {
        if (stretchSound != null)
        {
            stretchSound.Play();
        }
    }

    void PlayArrowRelease()
    {
        if (arrowRelease != null)
        {
            arrowRelease.Play();
        }
    }

    IEnumerator Cooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }

    void Shoot()
    {
        Instantiate(arrowPrefab, firePoint.position, firePoint.rotation, game.transform);
        arrowCount--;

        arrowManager.GetComponent<ArrowManager>().UpdateArrowCount(arrowCount);
    }

    public void addHeadshot()
    {
        headshots++;
    }
}
