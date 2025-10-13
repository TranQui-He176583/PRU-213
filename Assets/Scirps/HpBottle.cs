using UnityEngine;

public class HpBottle : MonoBehaviour
{
    [SerializeField] private float numberHeal = 25;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            player.heal(numberHeal);
            
        }
    }
}
