using UnityEngine;
using UnityEngine.InputSystem;

public class Движение : MonoBehaviour
{
    [Header("Настройки движения")]
    public PlayerStats stats;

    private Rigidbody2D физика;
    private Vector2 ввод;

    void Start()
    {
        физика = GetComponent<Rigidbody2D>();
        stats = GetComponent<PlayerStats>();
    }

    public void OnMove(InputValue value)
    {
        ввод = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        if (физика == null || stats == null) return;

        физика.linearVelocity = ввод * stats.moveSpeed;
    }
}