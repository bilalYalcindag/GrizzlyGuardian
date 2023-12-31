using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health = 100;
    public float speed = 5f;
    public int attackPower = 10;
    public Transform patrollingStartPoint;
    public Transform patrollingFinishPoint;

    private Transform target;
    private bool isPatrolling = true;

    void Start()
    {
        target = null;
    }

    void Update()
    {
        if (health <= 0)
        {
            // Eðer düþmanýn saðlýðý sýfýra düþtüyse, öldüðü kabul edilir.
            Die();
            return;
        }

        if (isPatrolling)
        {
            Patrol();
        }
        else if (target != null)
        {
            // Eðer bir hedef varsa, hedefe doðru hareket et
            MoveTowardsTarget();
        }
    }

    void Patrol()
    {
        // Dolaþma noktalarý arasýnda hareket et
        transform.position = Vector3.MoveTowards(transform.position, patrollingFinishPoint.position, speed * Time.deltaTime);

        // Eðer bitiþ noktasýna ulaþýldýysa, yönü deðiþtir
        if (Vector3.Distance(gameObject.transform.position, patrollingFinishPoint.position) < 0.1f)
        {
            
            Transform temp = patrollingStartPoint;
            patrollingStartPoint = patrollingFinishPoint;
            patrollingFinishPoint = temp;
        }

        // Eðer oyuncu düþmanýn görüþ alanýna girdiyse, dolaþmayý býrak
        Collider2D[] colliders = Physics2D.OverlapAreaAll(patrollingStartPoint.position, patrollingFinishPoint.position);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                isPatrolling = false;
                target = collider.transform;
                break;
            }
        }
    }

    void MoveTowardsTarget()
    {
        // Hedefe doðru hareket et
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // Eðer hedefe ulaþýldýysa, saldýrý yap
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            Attack();
        }
    }

    void Attack()
    {
        // Burada saldýrý logiðini ekle
        // Örneðin, target.GetComponent<PlayerController>().TakeDamage(attackPower);

        // Saldýrý sonrasýnda tekrar dolaþmaya baþla
        isPatrolling = true;
        target = null;
    }

    void Die()
    {
        
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health -= 50;
            Debug.Log("ates");
        }
    }
}
