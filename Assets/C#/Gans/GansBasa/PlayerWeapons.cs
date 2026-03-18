using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    public AutoTargetWeapon autoWeapon;
    public SwordWeapon swordWeapon;

    private void Start()
    {
        if (autoWeapon != null)
            autoWeapon.enabled = true;

        if (swordWeapon != null)
            swordWeapon.enabled = false;
    }

    public void UnlockSword()
    {
        if (swordWeapon != null && !swordWeapon.enabled)
        {
            swordWeapon.enabled = true;
            Debug.Log("Меч открыт");
        }
    }
}