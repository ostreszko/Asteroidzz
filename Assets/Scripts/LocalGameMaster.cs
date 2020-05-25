using UnityEngine;
using System;
using UnityEngine.UI;

//Class that handles various task in game
public class LocalGameMaster : MonoBehaviour
{

    float teleportOffset = 10f;
    [NonSerialized]
    public float topY, bottomY, leftX, rightX;
    [NonSerialized]
    public int enemiesLeft = 0, score = 0, lives = 3;
    [NonSerialized]
    public bool isPlayerDead = false, gameEnded = false, battleInProgress = false;
    [NonSerialized]
    public string deadPlayerInfoLabel = "Press R to respawn", defeatedWaveInfoLabel = "Press SPACE to next wave";
    [NonSerialized]
    public AudioManager audioManager;

    Vector2 respawnPosition = new Vector2(0, 0);

    public ScenesManagerController scenesManagerController;
    public System.Random rand = new System.Random();
    public Transform playerTransform;
    BoxCollider2D playerBoxCollider2D;
    SpriteRenderer playerSpriteRenderer;
    public static LocalGameMaster LGM;
    public GameObject infoLabelGameObject, guiPanelGameObject, endGamePanelGameObject, playerGameObject;
    public Text infoLabelText, endScoreText, playerLivesText, playerScoreText;


    private void Awake()
    {
        if (LGM != null)
        {
            GameObject.Destroy(LGM);
        }
        else
        {
            LGM = this;
        }
    }
    void Start()
    {
        Camera cam = Camera.main;
        Vector3 screenBottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0));
        Vector3 screenTopRight = cam.ViewportToWorldPoint(new Vector3(1, 1));
        leftX = screenBottomLeft.x - teleportOffset;
        rightX = screenTopRight.x + teleportOffset;
        topY = screenTopRight.y + teleportOffset;
        bottomY = screenBottomLeft.y - teleportOffset;
        playerLivesText.text = lives.ToString();

        playerBoxCollider2D = playerGameObject.GetComponent<BoxCollider2D>();
        playerSpriteRenderer = playerGameObject.GetComponent<SpriteRenderer>();
        audioManager = AudioManager.audioManager;

        GUIEvents.guiEvents.onScoreTrigger += ScoreGain;
        GUIEvents.guiEvents.onLiveDeplateTrigger += DeplateLive;
    }

    private void Update()
    {
        if(gameEnded && Input.GetKeyDown(KeyCode.R))
        {
            scenesManagerController.RestartLevel();
        }else if (gameEnded && Input.GetKeyDown(KeyCode.Escape))
        {
            scenesManagerController.LoadLevelByBuildIndex(0);
        }else if (isPlayerDead && Input.GetKeyDown(KeyCode.R))
        {
            PlayerRespawn();
        }
    }

    public void DeplateLive()
    {
        lives--;
        if (lives <= 0)
        {
            EndGame();
        }
        else
        playerLivesText.text = lives.ToString();
    }

    public void ScoreGain(int amount)
    {
        score += amount;
        playerScoreText.text = score.ToString();
    }

    private void PlayerRespawn()
    {
        playerGameObject.SetActive(true);
        playerTransform.position = respawnPosition;
        isPlayerDead = false;
        infoLabelGameObject.SetActive(false);
        battleInProgress = true;
        ActivateInvicible();
    }

    //After respawn makes player invincible for some time
    private void ActivateInvicible()
    {
        playerBoxCollider2D.enabled = false;
        Invoke("DisactivateInvicible", 2);
        playerSpriteRenderer.color = new Color(1,1,1,0.25f);
    }

    private void DisactivateInvicible()
    {
        playerBoxCollider2D.enabled = true;
        playerSpriteRenderer.color = new Color(1, 1, 1, 1f);
    }

    private void EndGame()
    {
        gameEnded = true;
        endScoreText.text = score.ToString();
        endGamePanelGameObject.SetActive(true);
        guiPanelGameObject.SetActive(false);
        battleInProgress = false;
        Time.timeScale = 0f;
    } 

    public void CheckEnemiesLeft()
    {
        if (enemiesLeft == 0)
        {
            battleInProgress = false;
            infoLabelText.text = defeatedWaveInfoLabel;
            infoLabelGameObject.SetActive(true);
        }
    }
}
