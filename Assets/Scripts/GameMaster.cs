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

    public static bool storyComplete = false;
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
        if (!playGame && MainMenu.newGame || GameOverUI.playAgain)
        {
            playGame = true;
            GameOverUI.playAgain = false;
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
    //public static bool isFemaleEnemyDead = false;

    public void EndGame()
    {
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
        gm.playerExplodeEffect(player);
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

    public void playerExplodeEffect(Player _player)
    {
        sfxMan.enemyExplode.Play();
        GameObject explosion = (GameObject)Instantiate(explosionRef);
        explosion.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y + 0.3f, _player.transform.position.z);
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

    public static void FemaleEnemyDefeat(FemaleEnemy femEnem)
    {
        //isFemaleEnemyDead = true;
        gm._FemaleEnemyDefeat(femEnem);
        //gm.StartCoroutine(gm.restartFemaleEnemy());
    }

    public void _FemaleEnemyDefeat(FemaleEnemy _femEnem)
    {
        scores += _femEnem.getScore;
        sfxMan.enemyExplode.Play();
        GameObject explosion = (GameObject)Instantiate(explosionRef);
        explosion.transform.position = new Vector3(_femEnem.transform.position.x, _femEnem.transform.position.y + 0.3f, _femEnem.transform.position.z);
        Destroy(_femEnem.gameObject);
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

    public static void XelciorDefeat(XelciorHealth xelcior)
    {
        gm._XelciorDefeat(xelcior);
    }

    public void _XelciorDefeat(XelciorHealth _xelcior)
    {
        scores += _xelcior.getScore;
        storyComplete = true;
        Destroy(_xelcior.gameObject, 6f);
    }

    public static void TsukimiDefeat(TsukimiHealth tsukimi)
    {
        gm._TsukimiDefeat(tsukimi);
    }

    public void _TsukimiDefeat(TsukimiHealth _tsukimi)
    {
        scores += _tsukimi.getScore;
        Destroy(_tsukimi.gameObject, 6f);
    }

    public static void HannaDefeat(HannaHealth hanna)
    {
        gm._HannaDefeat(hanna);
    }

    public void _HannaDefeat(HannaHealth _hanna)
    {
        scores += _hanna.getScore;
        Destroy(_hanna.gameObject, 6f);
    }

    public static void ManaDefeat(ManaHealth mana)
    {
        gm._ManaDefeat(mana);
    }

    public void _ManaDefeat(ManaHealth _mana)
    {
        scores += _mana.getScore;
        Destroy(_mana.gameObject, 6f);
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

    /*
    public IEnumerator restartFemaleEnemy()
    {
        yield return new WaitForSeconds(femaleEnemyRestart);
        isFemaleEnemyDead = false;
    }*/

        


}
