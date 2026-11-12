using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public playerMovement player;
    public WeaponScript weapon;

    public GameObject deathScreen;

    public Image healthBar;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1;

        player = GameObject.Find("player").GetComponent<playerMovement>();

        healthBar = GameObject.Find("HealthBar").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = (float)player.hp / (float)player.maxhp;

        if (player.hp <= 0)
        {
            //Time.timeScale = 0;
            //deathScreen.SetActive(true);           
        }
    }
}
