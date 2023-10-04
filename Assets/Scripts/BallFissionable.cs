using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallFissionable : Ball
{
    [SerializeField] private GameObject splitPrefab;

    override protected void Die()
    {
        if (size > 1)
        {
            SplitBalls();
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void SplitBalls()
    {
        GameObject newBall;
        for(int i = 0; i < 2; i++)
        {
            newBall = Instantiate(gameObject, transform.position, Quaternion.identity, BallSpawner.Instance.transform);
            newBall.GetComponent<Rigidbody2D>().velocity = new Vector2(leftAndRight[i], 5);
            newBall.GetComponent<BallFissionable>().health = initHealth / 2;
            if(newBall.GetComponent<BallFissionable>().health < 1)
            {
                newBall.GetComponent<BallFissionable>().health = 1;
            }
            newBall.GetComponent<BallFissionable>().isResultOfFission = true;
            newBall.GetComponent<BallFissionable>().size--;
        }
    }
}
