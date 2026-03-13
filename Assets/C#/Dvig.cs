using UnityEngine;
using UnityEngine.InputSystem;

public class Движение : MonoBehaviour
{
    [Header("НАстройки движения")]
    public float скорость = 10f;
    private Rigidbody2D физика;
    private Vector2 ввод;

    void Start()
    {
        физика = GetComponent<Rigidbody2D>();

        if (физика == null)
            Debug.LogError("Нет Rigidbody2D на корабле!");
    }


    public void OnMove(InputValue value)
    {
        ввод = value.Get<Vector2>();

        Debug.Log("Ввод:" + ввод);
        Debug.Log("OnMove вызван: " + ввод);
    }

    private void FixedUpdate()
    {
        if (this == null) return;
        if (gameObject == null) return;
        if (физика == null) return;

        физика.linearVelocity = ввод * скорость;
    }

}
