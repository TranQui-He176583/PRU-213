using UnityEngine;
using UnityEngine.UI;
public abstract class Bot : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 1f;
    protected Player player;
    [SerializeField] protected float maxHp =  50;
    protected float currentHp;
    [SerializeField] private Image hpBar;
    [SerializeField] private GameObject energyObject;
    protected virtual void Start()
    {

        player = FindAnyObjectByType<Player>();
        currentHp = maxHp;
        updateHpBar();

        // Khi enemy spawn, tự scale theo level hiện tại
        GameControl gc = FindAnyObjectByType<GameControl>();
        if (gc != null)
            ScaleStats(gc.getCurrentLevel());
    }

    protected virtual void Update()
    {

        moveToPlayer();
        flipEnemy(); 


    }
    protected void moveToPlayer()
    {
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }

    }
    protected void flipEnemy()
    {
        if (player != null)
        {
            transform.localScale = new Vector3(player.transform.position.x < transform.position.x ? -1 : 1, 1, 1);
        }
    }

    public virtual void takeDame(float damage)
    {
        currentHp-=damage; 
        currentHp = Mathf.Max(currentHp, 0);
        if (currentHp <= 0) 
        {
            Die();
        }
        updateHpBar();
    }

    protected void updateHpBar()
    {
        if (hpBar != null)
        {
            {
                hpBar.fillAmount = currentHp / maxHp;
            }
        }
    }
    protected virtual void Die()
    {

        if (energyObject != null)
        {
            GameObject energy = Instantiate(energyObject, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }


    public virtual void ScaleStats(int level)
    {
        // Tùy chọn tỉ lệ tăng
        float hpMultiplier = 1f + 0.2f * (level - 1);

        maxHp *= hpMultiplier;
        currentHp = maxHp;  // reset HP mỗi lần scale


        updateHpBar();
    }

}
