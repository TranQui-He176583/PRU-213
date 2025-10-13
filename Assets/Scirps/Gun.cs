using UnityEngine;
using TMPro;
public class Gun : MonoBehaviour
{
    [SerializeField] private Transform firePos;
    [SerializeField] private GameObject bulletPrefabs;
    [SerializeField] private float shotDelay = 0.15f;
    private float nextShot;
    [SerializeField] private int maxBullet = 24;
    [SerializeField] private int currentBullet;
    [SerializeField] private TextMeshProUGUI currentBulletText;


    void Start()
    {
        currentBullet = maxBullet;
    }


    void Update()
    {
        rotateGun();
        shootGun();
        reloadBullet();
        UpdateBulletText();
    }
    void rotateGun()
    {
        if (Input.mousePosition.x < 0 || Input.mousePosition.x > Screen.width || Input.mousePosition.y < 0 || Input.mousePosition.y > Screen.height) { return; }


        Vector3 displacement = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 180f);

        if (angle < -90 || angle > 90)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, -1, 1);
        }
    }

    void shootGun()
    {
        if (Input.GetMouseButtonDown(0) && currentBullet > 0 && Time.time > nextShot)
        {
            currentBullet--;
            nextShot = Time.time + shotDelay;
            Instantiate(bulletPrefabs, firePos.position, firePos.rotation);
            UpdateBulletText();
        }

    }

    void reloadBullet()
    {
        if (Input.GetKeyDown(KeyCode.R) && currentBullet < maxBullet)
        {
            currentBullet = maxBullet;
            UpdateBulletText();
        }
        
    }
    private void UpdateBulletText()
    {
        if (currentBulletText != null)
        {
            currentBulletText.text = currentBullet.ToString()+"/"+maxBullet;
        }
    }


}
