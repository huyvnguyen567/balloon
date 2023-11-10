using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;



public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public UnityEvent onStartGame = new UnityEvent(); // Sự kiện bấm chuột
    public static UnityEvent OnBuyClick = new UnityEvent();

    public enum GameState
    {
        MainMenu,
        Gameplay,
        Pause,
        Win,
        Lose
    }

    [SerializeField] private int currentLevel;
    public GameState currentGameState;
    [HideInInspector] public float ScreenWidth;

    public int ballSize1DestroyedCount;
    public float targetProcess;

    public GameObject bossPrefab;
    private GameObject bossClone;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        ScreenWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
    }

    private void Start()
    {
        onStartGame.AddListener(delegate
        { 
            Vector3 currentPosition = Camera.main.transform.position;
            Vector3 targetPosition = new Vector3(0, 0, -10);
            Camera.main.transform.DOMove(targetPosition, 0.5f).SetEase(Ease.Linear);
        });

        //DataManager.Instance.LoadLevel();
        //DataManager.Instance.LoadScore();
        //DataManager.Instance.LoadHighScore();
        //DataManager.Instance.LoadCoin();
        //DataManager.Instance.LoadDiamond();
        //DataManager.Instance.LoadNumberOfGamesPlayed();
        //DataManager.Instance.LoadBallDestroyedCount();
        //DataManager.Instance.LoadUpgradeData();
        //DataManager.Instance.LoadTaskTypeData();
        DataManager.Instance.LoadGameData();

        currentLevel = DataManager.Instance.currentLevel;

        StartLevel(currentLevel);
        SwitchGameState(GameState.MainMenu);

    }

   
    public void SwitchGameState(GameState newState)
    {
        switch (newState)
        {
            case GameState.MainMenu:
                UIManager.Instance.bigMainMenuPanel = UIManager.Instance.Spawn(UIType.Window, UIManager.Instance.bigMainMenuPanelPrefab);
                UIManager.Instance.processTaskPopup = UIManager.Instance.Spawn(UIType.Popup, UIManager.Instance.processTaskPopupPrefab);
                UIManager.Instance.cannonBuyPopup = UIManager.Instance.Spawn(UIType.Popup, UIManager.Instance.cannonBuyPopupPrefab);
                UIManager.Instance.menuContentPopup = UIManager.Instance.Spawn(UIType.Popup, UIManager.Instance.menuContentPopupPrefab);
                UIManager.Instance.comingSoonPopup = UIManager.Instance.Spawn(UIType.Popup, UIManager.Instance.comingSoonPopupPrefab);
                break;

            case GameState.Gameplay:
                UIManager.Instance.gamePlayWindow = UIManager.Instance.Spawn(UIType.Window, UIManager.Instance.gamePlayWindowPrefab);
                UpdateProcessGame();
                if ((DataManager.Instance.currentLevel % 5 == 0))
                {
                    SpawnBoss();
                }
                
                break;
            case GameState.Pause:
                // Xử lý khi dừng game
                break;
            case GameState.Lose:
                if (UIManager.Instance.losePopup == null)
                {
                    UIManager.Instance.losePopup = UIManager.Instance.Spawn(UIType.Popup, UIManager.Instance.losePopupPrefab);
                }

                break;
            case GameState.Win:
                if(UIManager.Instance.levelupPopup == null)
                {
                    UIManager.Instance.levelupPopup = UIManager.Instance.Spawn(UIType.Popup, UIManager.Instance.levelupPopupPrefab);
                }
                break;
        }
        currentGameState = newState;
        if (currentGameState == GameState.Gameplay)
        {
            ManagerAds.Ins.ShowBanner();
        }
        else
        {
            ManagerAds.Ins.HideBanner();
        }
    }

    public void StartLevel(int level)
    {
        LevelSO.Level levelData = DataManager.Instance.levelData.levels[level];

        currentLevel = levelData.level;
        BallSpawner.Instance.ballsCount = levelData.numberOfMeteor;
        BallSpawner.Instance.minHealth = levelData.minHealth;
        BallSpawner.Instance.maxHealth = levelData.maxHealth;
    }

    public void SpawnBoss()
    {
        GameObject bossClone = Instantiate(bossPrefab, new Vector3(0,7,0), Quaternion.identity);    
    }
    public ItemData GetRandomItem()
    {
        float totalChance = 0f;
        float randomValue = Random.Range(0f, 1f);

        foreach (var itemData in DataManager.Instance.items)
        {
            totalChance += itemData.chance;
            if (randomValue <= totalChance)
            {
                return itemData;
            }
        }

        return null;
    }

    public void UpdateProcessGame()
    {
        foreach(GameObject ball in BallSpawner.Instance.balls)
        {
            targetProcess += Mathf.Pow(2, ball.GetComponent<BallFissionable>().size - 1);
        }
    }

    public void BuyCannon(int index)
    {
        //CannonChangePanel cannonChangPanel = UIManager.Instance.bigMainMenuPanel.GetComponent<BigMainMenuPanel>().cannonPanel.GetComponent<CannonChangePanel>();
        List<CanonButtonSO> cannons = DataManager.Instance.cannonsData;

        CanonButtonSO cannonData = cannons[index];
        if (!cannonData.isPurchased && DataManager.Instance.diamond >= cannonData.price)
        {
            cannonData.isPurchased = true;
            DataManager.Instance.SaveCannonData(cannonData);
            DataManager.Instance.diamond -= cannonData.price;
            DataManager.Instance.SaveDiamond();
            UIManager.Instance.bigMainMenuPanel.GetComponent<BigMainMenuPanel>().UpdateDiamondText();
            OnBuyClick.Invoke();
        }
        else
        {
            Debug.Log("Không đủ tiền");
        }
    }
    public int CurrentLevel
    {
        get { return currentLevel; }
        set { currentLevel = value; }
    }
}
