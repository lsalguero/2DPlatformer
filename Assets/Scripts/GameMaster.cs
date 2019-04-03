using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

    public static GameMaster gm;

    private static int _remainingLives = 3;
    public static int RemainingLives {
        get { return _remainingLives; }
    }

    private void Awake() {
        if (gm == null) {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
    }

    public Transform playerPrefab;
    public Transform spawnPoint;
    public float spawnDelay = 2;
    public Transform spawnPrefab;
    public Transform respawnAudio;

    public Transform enemyDeathParticles;

    public CameraShake cameraShake;

    [SerializeField]
    private GameObject gameOverUI;

    private void Start() {
        if(cameraShake ==  null) {
            Debug.LogError("No camera shake reference in Game Master.");
        }
    }

    public void EndGame() {
        Debug.Log("Game over.");
        gameOverUI.SetActive(true);
    }

    public IEnumerator RespawnPlayer () {
        GetComponent<AudioSource>().Play();
        //  audio.Play();
        yield return new WaitForSeconds (spawnDelay);

        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        Transform clone = Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation) as Transform;
        Destroy(clone.gameObject, 3f);
    }

    public static void KillPlayer (Player player) {
        Destroy(player.gameObject);
        _remainingLives--;
        if(_remainingLives <= 0) {
            gm.EndGame();
        }
        else {
            gm.StartCoroutine(gm.RespawnPlayer());
        }
    }

    public static void KillEnemy(Enemy enemy) {
        gm._KillEnemy(enemy);
    }

    public void _KillEnemy(Enemy _enemy) {
        Transform _clone = Instantiate(_enemy.deathParticles, _enemy.transform.position, Quaternion.identity) as Transform;
        Destroy(_clone.gameObject, 5f);
        cameraShake.Shake(_enemy.shakeAmt, _enemy.shakeLength);
        Destroy(_enemy.gameObject);
    }
}
