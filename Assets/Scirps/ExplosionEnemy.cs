using UnityEngine;

public class ExplosionEnemy : Bot
{
    [SerializeField] private GameObject explosionPrefabs;
    private void CreateExplosion()
    {
        if (explosionPrefabs != null)
        {
            Instantiate(explosionPrefabs, transform.position, Quaternion.identity);
        }

    }
    protected override void Die()
    {
       CreateExplosion();
        base.Die();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CreateExplosion();
            base.Die();
        }
    }
}

