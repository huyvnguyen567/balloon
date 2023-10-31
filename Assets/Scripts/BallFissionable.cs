using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class BallFissionable : Ball
{
    [SerializeField] private GameObject ballPrefab;

    public static UnityEvent UpdateProcessEvent = new UnityEvent();

    override protected void Die()
    {
        ObjectPool.Instance.ReturnObjectToPool("Ball", gameObject);
        DataManager.Instance.ballDestroyedCount++;
        DataManager.Instance.SaveTaskTypeData(TaskType.BallBlasted, DataManager.Instance.ballDestroyedCount);
        if (size > minSize)
        {
            UpdateVisuals();
            SplitBalls();
        }
        else
        {
            DropItem();
            GameController.Instance.ballSize1DestroyedCount++;

            UpdateProcessEvent.Invoke();
        }
    }

    public void DropItem()
    {
        float random = UnityEngine.Random.value;
        if (random > 0.4f)
        {
            ItemData randomData = GameController.Instance.GetRandomItem();
            GameObject item = ObjectPool.Instance.GetObjectFromPool("Item");
            item.transform.position = transform.position;
            Item itemScript = item.GetComponent<Item>();
            itemScript.Initialize(randomData);
        }
    }

    private void SplitBalls()
    {
        for (int i = 0; i < 2; i++)
        {
            GameObject newBall = ObjectPool.Instance.GetObjectFromPool("Ball"); ;
            newBall.transform.position = transform.position;
            newBall.GetComponent<Rigidbody2D>().velocity = new Vector2(leftAndRight[i], 5);

            BallFissionable newBallFissionable = newBall.GetComponent<BallFissionable>();
            newBallFissionable.health = initHealth / 2;
            newBallFissionable.initHealth = newBallFissionable.health;
            if (newBallFissionable.health < 1)
            {
                newBallFissionable.health = 1;
            }
            newBallFissionable.size = initSize - 1;
            newBallFissionable.initSize = newBallFissionable.size;
            newBallFissionable.isResultOfFission = true;
            newBallFissionable.UpdateVisuals();
        }
    }
}
