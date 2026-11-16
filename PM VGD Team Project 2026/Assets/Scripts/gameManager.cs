using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using Unity.Cinemachine;

public class gameManager : MonoBehaviour
{
    public playerMovement player;
    public WeaponScript weapon;

    public GameObject deathScreen;

    public Image healthBar;

    public CinemachineCamera cameraFollow;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1;

        player = GameObject.Find("player").GetComponent<playerMovement>();

        deathScreen = GameObject.Find("DeathScreen");
        deathScreen.SetActive(false);

        healthBar = GameObject.Find("HealthBar").GetComponent<Image>();

        cameraFollow.Target.TrackingTarget = player.gameObject.transform;

        if (cameraFollow.Target.TrackingTarget == null)
        {
            Debug.Log("cameraFollow is NULL!");
        }
        if (player.gameObject.transform == null) {
            Debug.Log("player is NULL!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = (float)player.hp / (float)player.maxhp;

        if (player.hp <= 0)
        {
            Time.timeScale = 0;
            deathScreen.SetActive(true);           
        }
    }

    public void LoadLevel(int levelID)
    {
        if (levelID >= SceneManager.sceneCountInBuildSettings)
            Debug.Log("Level ID is too high: " + levelID);
        else
            SceneManager.LoadScene(levelID);
    }

    public void LoadCity()
    {
        LoadLevel(0);
    }
}
