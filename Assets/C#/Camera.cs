using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform цель;
    public float сглаживание = 5f;

    void LateUpdate()
    {
        if (цель == null) return;

        Vector3 новаяПозиция = new Vector3(цель.position.x, цель.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, новаяПозиция, сглаживание * Time.deltaTime);
    }


}
