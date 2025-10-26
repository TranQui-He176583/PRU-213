using UnityEngine;

public class ShooterEnemy : Bot
{
    [SerializeField] private GameObject energyObject;
    [SerializeField] private GameObject bulletPrefabs;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float speedBullet = 7f;
    protected float enterDame = 5;
    protected float stayDame = 1;
    [SerializeField] private float bulletCoolDown = 1f;
    private float nextbullet = 0f;


    protected override void Update()
    {
        base.Update();
        if (Time.time >= nextbullet)
        {
            shootBullet();
        }
    }
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

    private void normalShoot()
    {
        if (player != null)
        {
            Vector3 directionToPlayer = player.transform.position - firePoint.position;
            directionToPlayer.Normalize();
            GameObject bullet = Instantiate(bulletPrefabs, firePoint.position, Quaternion.identity);
            EnenmyBullet enenmyBullet = bullet.AddComponent<EnenmyBullet>();
            enenmyBullet.setMovementDirection(directionToPlayer * speedBullet);
        }

    }

    private void shootBullet()
    {
        normalShoot();
        nextbullet = Time.time + bulletCoolDown;
    }

    public override void ScaleStats(int level)
    {
        base.ScaleStats(level);
        enterDame *= 1f + 0.2f * (level - 1);
        stayDame *= 1f + 0.2f * (level - 1);
    }



}
