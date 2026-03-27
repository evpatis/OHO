using System;
using UnityEngine;

public class SkeletonBomb : MonoBehaviour
{
    private float moveSpeed;
    private float explodeDelay;
    private float explodeRadius;
    private int explodeDamage;

    private float timer;
    private Rigidbody2D rb;
    private Action onFinished;

    private float stopDistance = 0.4f;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    [Header("Эффекты перед взрывом")]
    public float warningTime = 1f;
    public float shakeAmount = 0.08f;
    public Color warningColor = Color.red;

    private Vector3 originalLocalPosition;

    public void Setup(
        float moveSpeedValue,
        float explodeDelayValue,
        float explodeRadiusValue,
        int explodeDamageValue,
        Action finishedCallback
    )
    {
        moveSpeed = moveSpeedValue;
        explodeDelay = explodeDelayValue;
        explodeRadius = explodeRadiusValue;
        explodeDamage = explodeDamageValue;
        onFinished = finishedCallback;

        timer = explodeDelay;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;

        originalLocalPosition = transform.localPosition;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            Explode();
            return;
        }

        if (timer <= warningTime)
        {
            WarningEffect();
        }
    }

    private void FixedUpdate()
    {
        if (rb == null) return;
        if (timer <= warningTime)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        GameObject targetEnemy = FindNearestEnemy();

        if (targetEnemy == null)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 targetPos = targetEnemy.transform.position;
        Vector2 myPos = rb.position;

        float distance = Vector2.Distance(myPos, targetPos);

        if (distance <= stopDistance)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 dir = (targetPos - myPos).normalized;
        rb.linearVelocity = dir * moveSpeed;

        if (dir.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (dir.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    void WarningEffect()
    {
        if (spriteRenderer != null)
        {
            float blink = Mathf.PingPong(Time.time * 8f, 1f);
            spriteRenderer.color = Color.Lerp(originalColor, warningColor, blink);
        }

        float shakeX = UnityEngine.Random.Range(-shakeAmount, shakeAmount);
        float shakeY = UnityEngine.Random.Range(-shakeAmount, shakeAmount);
        transform.localPosition = originalLocalPosition + new Vector3(shakeX, shakeY, 0f);
    }

    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Vrag");

        GameObject nearest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            if (enemy == null || !enemy.activeInHierarchy) continue;

            float dist = Vector2.Distance(transform.position, enemy.transform.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                nearest = enemy;
            }
        }

        return nearest;
    }

    void Explode()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explodeRadius);

        foreach (Collider2D hit in hits)
        {
            if (!hit.CompareTag("Vrag")) continue;

            Vrag enemy = hit.GetComponent<Vrag>();
            if (enemy != null)
            {
                enemy.ПолучитьУрон(explodeDamage);
            }
        }

        onFinished?.Invoke();
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (spriteRenderer != null)
            spriteRenderer.color = originalColor;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, explodeRadius);
    }
}