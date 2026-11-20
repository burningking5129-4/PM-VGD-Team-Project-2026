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

    public CinemachineCamera camera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 5)
        {
            deathScreen = GameObject.Find("DeathScreen");
            deathScreen.SetActive(false);
            DontDestroyOnLoad(camera);
            Time.timeScale = 1;

            player = GameObject.Find("player").GetComponent<playerMovement>();

            healthBar = GameObject.Find("HealthBar").GetComponent<Image>();

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)player.hp / (float)player.maxhp;
        }

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

    public void quit()
    {
        Application.Quit();
    }
}
