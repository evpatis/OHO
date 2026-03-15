using UnityEngine;

public class Душа : MonoBehaviour
{
    public int опыт = 1;
    public float радиусПритяжения = 3f;
    public float скоростьПритяжения = 5f;
    public float дистанцияПодбора = 0.3f;

    private Transform игрок;
    private ОпытИгрока опытИгрока;
    private bool подобрана = false;

    void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            игрок = playerObj.transform;
            опытИгрока = playerObj.GetComponent<ОпытИгрока>();
        }
    }

    public void УстановитьОпыт(int значение)
    {
        опыт = значение;
    }

    void Update()
    {
        if (игрок == null || подобрана) return;

        float дистанция = Vector2.Distance(transform.position, игрок.position);

        if (дистанция <= радиусПритяжения)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                игрок.position,
                скоростьПритяжения * Time.deltaTime
            );
        }

        if (дистанция <= дистанцияПодбора)
        {
            Подобрать();
        }
    }

    void Подобрать()
    {
        if (подобрана) return;
        подобрана = true;

        if (опытИгрока != null)
        {
            опытИгрока.ДобавитьОпыт(опыт);
        }

        Destroy(gameObject);
    }
}
