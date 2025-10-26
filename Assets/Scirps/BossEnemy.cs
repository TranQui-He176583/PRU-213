using Unity.VisualScripting;
using UnityEngine;

public class BossEnemy : Bot
{
    protected float enterDame = 20;
    protected float stayDame = 3;
    [SerializeField] private GameObject bulletPrefabs;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float speedBullet = 7f;
    [SerializeField] private float hpHeal=100f;
    [SerializeField] private GameObject miniE;
    [SerializeField] private float skillCoolDown =1f;
    private float nextSkill =0f;

    protected override void Update()
    {
        base.Update();
       if (Time.time >= nextSkill )
        {
            useSkill();
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
        if (player != null) {
            Vector3 directionToPlayer = player.transform.position - firePoint.position;
            directionToPlayer.Normalize();
            GameObject bullet = Instantiate(bulletPrefabs, firePoint.position, Quaternion.identity);
            EnenmyBullet enenmyBullet = bullet.AddComponent<EnenmyBullet>();
            enenmyBullet.setMovementDirection(directionToPlayer*speedBullet);
        }


    }

    private void specialShoot()
    {
        const int numberBullet = 12;
        float angleStep = 360f / numberBullet;
        for (int i = 0; i < numberBullet; i++)
        {
            float angle = i * angleStep;
            Vector3 bulletDirection = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad *angle), 0);
            GameObject bullet = Instantiate(bulletPrefabs, transform.position, Quaternion.identity);
            EnenmyBullet enenmyBullet = bullet.AddComponent<EnenmyBullet>();
            enenmyBullet.setMovementDirection(bulletDirection * speedBullet);


        }
    }
    private void Heal(float hpHeal)
    {
        currentHp = Mathf.Min(currentHp+hpHeal, maxHp);
        updateHpBar();
    }
    private void createMini()
    {
        Instantiate(miniE, transform.position, Quaternion.identity);
    }

    private void randomSkill()
    {
        int randomSkill = Random.Range(0, 4);
        switch (randomSkill)
        {
            case 0:
                normalShoot(); break;
            case 1:
                specialShoot(); break;
            case 2:
                Heal(hpHeal);
                break;
            case 3:
                createMini();
                break;
        }

    }
    private void useSkill()
    {
        nextSkill = Time.time +skillCoolDown;
        randomSkill();
    }

    public override void ScaleStats(int level)
    {
        base.ScaleStats(level);

        // Tỉ lệ tăng theo cấp 
        float damageMultiplier = 1f + 0.25f * (level - 1);
        float cooldownMultiplier = Mathf.Max(0.8f, 1f - 0.05f * (level - 1)); // giảm 5% cooldown mỗi level, tối đa còn 80%

        // Tăng chỉ số
        enterDame *= damageMultiplier;
        stayDame *= damageMultiplier;
        skillCoolDown *= cooldownMultiplier;
        hpHeal *= 1f + 0.15f * (level - 1); // mỗi cấp boss hồi được nhiều hơn

        Debug.Log($"[BossEnemy] Scaled to level {level}. HP={maxHp}, Damage={enterDame}, Cooldown={skillCoolDown}");
    }

}
