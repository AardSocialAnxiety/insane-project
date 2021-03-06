﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed;
    public float distance;

    private bool movingLeft = true;

    public Transform groundDetection;
    public Transform obstacleDetection;

    public int health;
    private int count = 0;

    private float dazedTime;
    public float startDazedTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health<=0)
        {
            Destroy(gameObject);
        }

        if(dazedTime<=0)
        {
            speed = 5;
        }
        else
        {
            speed = 0;
            dazedTime -= Time.deltaTime;
        }
        transform.Translate(Vector2.right * speed * Time.deltaTime);


        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        RaycastHit2D obstacleInfo = Physics2D.Raycast(obstacleDetection.position, Vector2.left, distance);
        if (groundInfo.collider == false || (obstacleInfo.collider == true && !obstacleInfo.collider.gameObject.tag.Equals("Player")))
        {
            if(movingLeft == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingLeft = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingLeft = true;
            }
        }
    }

    public void takeDamage(int damage)
    {
        dazedTime = startDazedTime;
        count++;
        health -= damage;
        Debug.Log("Damage Taken! "+count);
    }
}
