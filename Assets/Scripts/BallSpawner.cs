using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public static BallSpawner Instance;

    [SerializeField] private int ballsCount;
    [SerializeField] private float spawnDelay;
    [SerializeField] private int maxHealth;

    private GameObject[] balls;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        PrepareBalls();
        GameController.Instance.onStartGame.AddListener(delegate { StartCoroutine(SpawnBalls()); });
        //StartCoroutine(SpawnBalls());
    }
 
    IEnumerator SpawnBalls()
    {
        for (int i = 0; i < ballsCount; i++)
        {
            balls[i].SetActive(true);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public void PrepareBalls()
    {
        balls = new GameObject[ballsCount];
        //int prefabsCount = ballPrefabs.Length;
        for (int i = 0; i < ballsCount; i++)
        {
            balls[i] = ObjectPool.Instance.GetObjectFromPool("Ball");
            BallFissionable ball = balls[i].GetComponent<BallFissionable>();
            ball.size = Random.Range(1, 5);
            ball.health = Random.Range(1, maxHealth);
            ball.isResultOfFission = false;
            balls[i].SetActive(false);
        }
    }
}
