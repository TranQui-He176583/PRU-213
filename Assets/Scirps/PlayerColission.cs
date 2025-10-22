using UnityEngine;

public class PlayerColission : MonoBehaviour
{
    [SerializeField] private GameControl gameControl;
    [SerializeField] private AudioManager audioManager;
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
            audioManager.PlayEnergySound();
        } else if (collision.CompareTag("Heal"))
        {
            player.heal(20);
        }
    }
}
