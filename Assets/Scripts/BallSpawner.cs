using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public static BallSpawner Instance;

    [SerializeField] private GameObject[] ballPrefabs;
    [SerializeField] private int ballsCount;
    [SerializeField] private float spawnDelay;

    private GameObject[] balls;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        PrepareBalls();
        StartCoroutine(SpawnBalls());
    }

    IEnumerator SpawnBalls()
    {
        for(int i = 0; i < ballsCount; i++)
        {
            balls[i].SetActive(true);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public void PrepareBalls()
    {
        balls = new GameObject[ballsCount];
        int prefabsCount = ballPrefabs.Length;
        for(int i = 0; i < ballsCount; i++)
        {
            balls[i] = Instantiate(ballPrefabs[Random.Range(0, prefabsCount)], transform);
            balls[i].GetComponent<BallFissionable>().size = Random.Range(1, 5);
            balls[i].GetComponent<BallFissionable>().health = Random.Range(1, 20);
            balls[i].GetComponent<Ball>().isResultOfFission = false;
            balls[i].SetActive(false);
        }
    }
}
