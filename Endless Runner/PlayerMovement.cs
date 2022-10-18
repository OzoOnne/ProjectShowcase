using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Lanes
{
    Middle,
    Right,
    Left
}

public class PlayerMovement : MonoBehaviour
{
    [Tooltip("How fast the player moves")]
    [SerializeField] private float speed = 5;
    [Tooltip("How much the player can move side ways")]
    [SerializeField] private float sideMovement = 5;
    [Tooltip("How far the player can move to the sides")]
    [SerializeField] private float clampValue = 5;
    [Tooltip("How smooth the side movement is")]
    [SerializeField] private float sideSmooth = 3;
    [Tooltip("A multiplier for the side movement")]
    [SerializeField] private AnimationCurve speedMultiplier;

    private PlayerControls playerControls;
    private Rigidbody rb;
    private float newXPosition;
    private float xMovement;
    private Lanes currentLane;

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody>();
        currentLane = Lanes.Middle;
        newXPosition = transform.position.x;
    }

    private void Update()
    {
        if (GameManager.instance.GetGameState() != GameState.Game)
            return;

        Move();
    }

    private void Move()
    {
        if (playerControls.Player.Left.triggered)
        {
            if (currentLane == Lanes.Middle)
            {
                newXPosition = -sideMovement;
                currentLane = Lanes.Left;
            }
            else if (currentLane == Lanes.Right)
            {
                newXPosition = 0;
                currentLane = Lanes.Middle;
            }
        }
        if (playerControls.Player.Right.triggered)
        {
            if (currentLane == Lanes.Middle)
            {
                newXPosition = sideMovement;
                currentLane = Lanes.Right;
            }
            else if (currentLane == Lanes.Left)
            {
                newXPosition = 0;
                currentLane = Lanes.Middle;
            }
        }

        xMovement = Mathf.Lerp(xMovement, newXPosition, Time.deltaTime * sideSmooth * speedMultiplier.Evaluate(Time.deltaTime));
        transform.position = new Vector3(xMovement, transform.position.y, transform.position.z);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -clampValue, clampValue), transform.position.y, transform.position.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
            GameManager.instance.SetGameOver();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.GetComponent<SimplePool.PoolItem>().ReturnToPool();
            ShopManager.Instance.CoinsChanger(1);
        }
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
}
