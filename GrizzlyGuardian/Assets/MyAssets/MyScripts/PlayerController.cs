using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int health = 100;
    public int speed = 5 ;
    public int attackPower = 20;
    public float jumpPower = 250;
    public GameObject[] playerInventory = new GameObject[5];
    public GameObject bulletObject;
    public float bulletSpeed;
    public Transform shotPoint;
    public Animator animator;

    private Rigidbody2D rb;
    private bool groundControl  = false;
    private int jumpCount = 0;
    private int sayac = 0;

  
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {
        PlayerMoving();
        PlayerAttack();
    }
   
    public void PlayerMoving()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * speed * Time.deltaTime;
        transform.Translate(movement);

        if (Input.GetKeyDown(KeyCode.Space) && groundControl == true)
        {
            rb.AddForce(new Vector2(0, jumpPower));
            jumpCount++;

            if (jumpCount > 1)
            {
                groundControl = false;

            }
        }

        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetBool("yurume", true);
          
        }

       else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
           animator.SetBool("yurume",false);
        }


    }

    public void InventoryCollect()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Element"))
        {
            playerInventory[sayac] = collision.gameObject;
            collision.gameObject.SetActive(false);
            sayac += 1;

        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision2)
    {
        
        if (collision2.gameObject.CompareTag("Ground"))
        {
           
            groundControl = true;
            jumpCount = 0;
        }
    }

    public void PlayerAttack()
    {
      if(Input.GetMouseButtonDown(0))
        {
            GameObject mermi = Instantiate(bulletObject, shotPoint.position, shotPoint.rotation);

            // Mermiye hýz ver
            Rigidbody2D rb = mermi.GetComponent<Rigidbody2D>();
            rb.velocity = shotPoint.right *bulletSpeed;

            // 2 saniye sonra mermiyi yok et
            Destroy(mermi, 1f);
        }
    }
    
}
