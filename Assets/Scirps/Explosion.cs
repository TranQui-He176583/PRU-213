using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float dame = 25;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        Bot Enemy = collision.GetComponent<Bot>();
        if (collision.CompareTag("Player"))
        {
            player.takeDame(dame);
        }
        if (collision.CompareTag("Enemy"))
        {
            Enemy.takeDame(dame);
        }
    }
    public void DestroyExplosion()
    {
        Destroy(gameObject);
    }
}
