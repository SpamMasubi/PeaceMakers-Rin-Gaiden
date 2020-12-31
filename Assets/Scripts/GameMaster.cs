using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    [SerializeField]
    private int maxLives = 3;

    public static GameMaster gm;

    public static SFXManager sfxMan;

    public StatusIndicator statusIndicator;

    private static int _playerLives = 3;
    public static int playerLives
    {
        get { return _playerLives;  }
    }

    [SerializeField]
    private int startingScore = 0;
    public static int scores;
    private UnityEngine.Object explosionRef;

    public static bool playGame = false;
    void Awake()
    {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
        sfxMan = FindObjectOfType<SFXManager>();
    }

    void Start()
    {
        explosionRef = Resources.Load("EnemyExplode");
        if (!playGame)
        {
            playGame = true;
            _playerLives = maxLives;
            scores = startingScore;
        }
    }

    public Transform playerPrefab;
    public Transform spawnPoint;
    public float spawnDelay = 3.0f;
    public float gameOverDelay = 3.0f;
    public Transform spawnPrefab;

    public string sceneToLoad;

    public static bool charaDead = false;
    public static bool isGameOver = false;

    public void EndGame()
    {
        Boss.startBoss = false;
        Commander.startCommanderBoss = false;
        SceneManager.LoadScene(sceneToLoad);

    }

    public IEnumerator RespawnPlayer()
    {
        if (!isGameOver)
        {
            yield return new WaitForSeconds(spawnDelay);
            Transform player = (Transform)Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
            player.GetComponent<Player>().statsInd = this.statusIndicator;
            Transform clone = (Transform)Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation);
            sfxMan.spawning.Play();
            sfxMan.playerStart.Play();
            Destroy(clone.gameObject, 3.0f);
            charaDead = false;
        }
        else
        {
            yield return new WaitForSeconds(gameOverDelay);
            charaDead = false;
            gm.EndGame();
        }
    }

    public static void KillPlayer(Player player)
    {
        Destroy(player.gameObject);
        if(_playerLives < 1)
        {
            charaDead = true;
            isGameOver = true;
            playGame = false;
            sfxMan.playerDead.Play();
            gm.StartCoroutine(gm.RespawnPlayer());
        }
        else
        {
            _playerLives -= 1;
            charaDead = true;
            gm.StartCoroutine(gm.RespawnPlayer());
        }
        
    }
    public static void KillEnemy(Enemy enemy)
    {
        gm._KillEnemy(enemy);
    }

    public void _KillEnemy(Enemy _enemy)
    {
        scores += _enemy.getScore;
        sfxMan.enemyExplode.Play();
        GameObject explosion = (GameObject)Instantiate(explosionRef);
        explosion.transform.position = new Vector3(_enemy.transform.position.x, _enemy.transform.position.y + 0.3f, _enemy.transform.position.z);
        Destroy(_enemy.gameObject);
    }

    public static void KillBoss(BossHealth boss)
    {
        gm._KillBoss(boss);
    }

    public void _KillBoss(BossHealth _boss)
    {
        scores += _boss.getScore;
        sfxMan.enemyExplode.Play();
        GameObject explosion = (GameObject)Instantiate(explosionRef);
        explosion.transform.position = new Vector3(_boss.transform.position.x, _boss.transform.position.y + 0.3f, _boss.transform.position.z);
        Destroy(_boss.gameObject);
    }

    public static void CommanderDefeat(CommanderHealth commander)
    {
        gm._CommanderDefeat(commander);
    }

    public void _CommanderDefeat(CommanderHealth _commander)
    {
        scores += _commander.getScore;
        Destroy(_commander.gameObject, 6f);
    }

    public static void GetPoints(Points point)
    {
        gm._Points(point);
    }

    public void _Points(Points _point)
    {
        scores += _point.pointsValue;
        sfxMan.points.Play();
        Destroy(_point.gameObject);
    }

    public static void GetPlayerLives(Player1UpLife playerLives)
    {
        gm._GetPlayerLives(playerLives);
    }

    public void _GetPlayerLives(Player1UpLife playerLives)
    {
        _playerLives += playerLives.lifeValue;
        sfxMan.player1UP.Play();
        Destroy(playerLives.gameObject);
    }

        


}
