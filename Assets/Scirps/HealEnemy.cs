using UnityEngine;

public class HealEnemy : Bot
{
    [SerializeField] private GameObject healObject;
    protected float enterDame = 10;
    protected float stayDame = 2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.takeDame(enterDame);
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.takeDame(stayDame);
        }
    }
    protected override void Die()
    {
        if (healObject != null)
        {
            GameObject heal = Instantiate(healObject, transform.position, Quaternion.identity);
        }
        base.Die();
    }
}
