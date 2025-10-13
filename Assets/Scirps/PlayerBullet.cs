using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private float timeClear = 0.75f;
    [SerializeField] private float currentDame = 10;
    [SerializeField] GameObject bloodPrefabs;
    void Start()
    {
        Destroy(gameObject,timeClear);
    }



    void Update()
    {
        fireBullet();
    }
    void fireBullet()
    {
        transform.Translate(Vector2.right * Time.deltaTime*moveSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Bot bot = collision.GetComponent<Bot>();
            if (bot != null) 
            {
                bot.takeDame(currentDame);              
                GameObject blood = Instantiate(bloodPrefabs ,transform.position, Quaternion.identity);
                Destroy(blood, 0.5f);
                Destroy(gameObject);
            }
           
        }

    }
}
