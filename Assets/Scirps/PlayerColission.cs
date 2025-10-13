using UnityEngine;

public class PlayerColission : MonoBehaviour
{
    [SerializeField] private GameControl gameControl;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = GetComponent<Player>();
        if (collision.CompareTag("EnemyBullet"))
        {
           
            player.takeDame(10f);
        } else if (collision.CompareTag("Energy"))
        {
            gameControl.addExp();
            Destroy(collision.gameObject);
        } else if (collision.CompareTag("Heal"))
        {
            player.heal(20);
        }
    }
}
