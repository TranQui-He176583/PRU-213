using UnityEngine;
public class BasicEnemy : Bot
{
    protected float enterDame = 5;
    protected float stayDame = 1;
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

    public override void ScaleStats(int level)
    {
        base.ScaleStats(level);
        enterDame *= 1f + 0.2f * (level - 1);
        stayDame *= 1f + 0.2f * (level - 1);
    }


}
