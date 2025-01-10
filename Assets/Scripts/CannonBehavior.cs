using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBehavior : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private int  bulletCounter;

    private Queue<GameObject> magazine;

    private GameObject currentBullet;

    [SerializeField] private float bulletVelocity;

    private float timer = 3;
    private float currentTimer = 0;

    [SerializeField] private GameObject bulletPos;

    void Start()
    {
        CreateBullets();
    }
    
    private void Update()
    {
        if (currentTimer <= timer)
        {
            currentTimer += Time.deltaTime;

            if (currentTimer >= timer)
            {
                currentTimer = 0;
                Shoot();
            }
        }
    }

    private void CreateBullets()
    {
        magazine = new Queue<GameObject>();

        for (int i = 0; i < bulletCounter; i++)
        {
            GameObject b = Instantiate(bullet);
            b.transform.parent = bulletPos.transform;
            b.SetActive(false);
            magazine.Enqueue(b);
        }
    }

    private void Shoot()
    {
        currentBullet = magazine.Dequeue();

        currentBullet.SetActive(true);

        currentBullet.transform.position = bulletPos.transform.position;

        Rigidbody rb = currentBullet.GetComponent<Rigidbody>();

        rb.velocity = transform.forward * bulletVelocity;

        magazine.Enqueue(currentBullet);
    }
}
