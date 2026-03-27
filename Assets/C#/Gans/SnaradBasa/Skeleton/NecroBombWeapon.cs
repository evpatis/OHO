using UnityEngine;

public class NecroBombWeapon : WeaponBase
{
    public GameObject skeletonPrefab;

    public float searchRadius = 20f;
    public float skeletonMoveSpeed = 2.5f;
    public float explodeDelay = 3f;
    public float explodeRadius = 2.5f;
    public int explodeDamage = 10;

    private SkeletonBomb currentSkeleton;

    protected override void Attack()
    {
        if (currentSkeleton != null) return;

        if (skeletonPrefab == null)
        {
            Debug.LogError("Skeleton Prefab не назначен");
            return;
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Vrag");
        if (enemies.Length == 0) return;

        GameObject randomEnemy = enemies[Random.Range(0, enemies.Length)];
        if (randomEnemy == null) return;

        Vector3 spawnPosition = randomEnemy.transform.position;

        GameObject skeletonObj = Instantiate(skeletonPrefab, spawnPosition, Quaternion.identity);

        SkeletonBomb skeleton = skeletonObj.GetComponent<SkeletonBomb>();
        if (skeleton == null)
        {
            Debug.LogError("На skeletonPrefab нет SkeletonBomb");
            return;
        }

        skeleton.Setup(
            skeletonMoveSpeed,
            explodeDelay,
            explodeRadius,
            explodeDamage,
            OnSkeletonFinished
        );

        currentSkeleton = skeleton;

        Debug.Log("Союзный скелет появился");
    }

    private void OnSkeletonFinished()
    {
        currentSkeleton = null;
    }
}