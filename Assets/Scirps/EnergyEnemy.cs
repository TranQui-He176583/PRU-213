using UnityEngine;

public class EnergyEnemy : Bot
{
    [SerializeField] private GameObject energyObject;
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
    //protected override void Die()
    //{
    //    if (energyObject != null) 
    //    { 
    //        GameObject energy =   Instantiate(energyObject,transform.position, Quaternion.identity);
    //    }
    //    base.Die();
    //}
}
