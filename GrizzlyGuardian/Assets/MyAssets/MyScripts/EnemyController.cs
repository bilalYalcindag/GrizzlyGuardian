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
            // E�er d��man�n sa�l��� s�f�ra d��t�yse, �ld��� kabul edilir.
            Die();
            return;
        }

        if (isPatrolling)
        {
            Patrol();
        }
        else if (target != null)
        {
            // E�er bir hedef varsa, hedefe do�ru hareket et
            MoveTowardsTarget();
        }
    }

    void Patrol()
    {
        // Dola�ma noktalar� aras�nda hareket et
        transform.position = Vector3.MoveTowards(transform.position, patrollingFinishPoint.position, speed * Time.deltaTime);

        // E�er biti� noktas�na ula��ld�ysa, y�n� de�i�tir
        if (Vector3.Distance(gameObject.transform.position, patrollingFinishPoint.position) < 0.1f)
        {
            
            Transform temp = patrollingStartPoint;
            patrollingStartPoint = patrollingFinishPoint;
            patrollingFinishPoint = temp;
        }

        // E�er oyuncu d��man�n g�r�� alan�na girdiyse, dola�may� b�rak
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
        // Hedefe do�ru hareket et
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // E�er hedefe ula��ld�ysa, sald�r� yap
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            Attack();
        }
    }

    void Attack()
    {
        // Burada sald�r� logi�ini ekle
        // �rne�in, target.GetComponent<PlayerController>().TakeDamage(attackPower);

        // Sald�r� sonras�nda tekrar dola�maya ba�la
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
