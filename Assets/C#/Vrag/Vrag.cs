using UnityEngine;

public class Vrag : MonoBehaviour
{
    public float скорость = 0.5f;
    public Transform цель;
    public Rigidbody2D физика;

    public int максимальноеЗдоровье = 3;
    public int текущееЗдоровье;

    public GameObject душаПрефаб;
    public int количествоОпыта = 1;

    public int уронКасанием = 1;
    public float задержкаМеждуАтаками = 1f;

    private float таймерАтаки;
    private bool мертв = false;

    void Start()
    {
        текущееЗдоровье = максимальноеЗдоровье;
        физика = GetComponent<Rigidbody2D>();

        GameObject игрок = GameObject.FindWithTag("Player");
        if (игрок != null)
            цель = игрок.transform;
    }

    void Update()
    {
        if (таймерАтаки > 0)
            таймерАтаки -= Time.deltaTime;
    }

    public void ПолучитьУрон(int урон)
    {
        if (мертв) return;

        текущееЗдоровье -= урон;

        if (текущееЗдоровье <= 0)
        {
            Смерть();
        }
    }
    
    void Смерть()
    {
        if (мертв) return;
        мертв = true;

        if (душаПрефаб != null)
        {
            GameObject душа = Instantiate(душаПрефаб, transform.position, Quaternion.identity);

            Душа душаСкрипт = душа.GetComponent<Душа>();
            if (душаСкрипт != null)
            {
                душаСкрипт.УстановитьОпыт(количествоОпыта);
            }
        }

        Destroy(gameObject);
    }
    
   
    void FixedUpdate()
    {
        if (мертв) return;
        if (цель == null) return;
        if (!цель.gameObject.activeInHierarchy) return;

        Vector2 направление = (цель.position - transform.position).normalized;
        физика.linearVelocity = направление * скорость;

        Collider2D[] соседи = Physics2D.OverlapCircleAll(transform.position, 1f);
        foreach (Collider2D сосед in соседи)
        {
            if (сосед.gameObject != gameObject && сосед.CompareTag("Vrag"))
            {
                Vector2 оттолкнуть = (transform.position - сосед.transform.position).normalized;
                физика.AddForce(оттолкнуть * 10f);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        if (таймерАтаки > 0) return;

        ИгрокЗдоровье игрок = collision.gameObject.GetComponent<ИгрокЗдоровье>();
        if (игрок != null)
        {
            игрок.ПолучитьУрон(уронКасанием);
            таймерАтаки = задержкаМеждуАтаками;
        }
    }
}