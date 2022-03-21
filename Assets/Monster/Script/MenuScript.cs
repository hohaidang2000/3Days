using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class MenuScript : MonoBehaviour
{
    public GameObject playerObject;
    public CharacterController playerController;
    public int count = 0;
    public int winCount = 20;
    public PlayerController player;
    MenuInput playerInput;
    public bool running = false;
    public GameObject pauseMenu;
    public GameObject winMenu;
    public GameObject loseMenu;
    public bool pause = false;
    public TextMeshProUGUI winGui;
    public TextMeshProUGUI healthGui;
    // Start is called before the first frame update
    void Awake()
    {   

        playerInput = new MenuInput();
        Time.timeScale = 1;
        player.enabled = true;
        winGui.enabled = true;
        playerInput.MenuController.Enable();
        playerInput.MenuController.Pause.started += contex =>
        {
           
            if (!running) { 
                if(pause == false)
                {
                    
                    pause = true;
                    Time.timeScale = 0;
                    Cursor.lockState = CursorLockMode.None;
                    pauseMenu.SetActive(pause);
                    player.enabled = false;
                    winGui.enabled = false;
                }
                else
                {
                    pause = false;
                    Time.timeScale = 1;
                    Cursor.lockState = CursorLockMode.Locked;
                    pauseMenu.SetActive(pause);
                    player.enabled = true;
                    winGui.enabled = true;
                }
            }
        };
    //animator = GetComponent<Animator>();
    }
    public void Count()
    {
        count += 1;
    }

    void Start()
    {
        
    }
    public void loseHappen()
    {
        running = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        loseMenu.SetActive(true);
        winGui.enabled = false;
        player.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        healthGui.SetText(playerObject.GetComponent<Health>()._health.ToString());
        if(playerObject.GetComponent<Health>()._health <= 0){
            loseHappen();
        }
        winGui.SetText(count + " / " + winCount);
        if(count == winCount)
        {
            WinHappen();
        }
    }
    public void WinHappen()
    {
        running = true;
        
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        winMenu.SetActive(true);
        player.enabled = false;
        winGui.enabled = false;
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void Restart()
    {
        Time.timeScale = 1;
        player.enabled = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Continue()
    {
        pause = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        player.enabled = true;
        winGui.enabled = true;
    }
    public void Quit()
    {
        Application.Quit();
    }
}
