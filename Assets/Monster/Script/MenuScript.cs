using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuScript : MonoBehaviour
{
    MenuInput playerInput;
    public bool running = false;
    public GameObject pauseMenu;
    public bool pause = false;
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new MenuInput();

        playerInput.MenuController.Enable();
        playerInput.MenuController.Pause.started += contex =>
        {
            if(pause == false)
            {
                
                pause = true;
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                pauseMenu.SetActive(pause);
            }
            else
            {
                pause = false;
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                pauseMenu.SetActive(pause);
            }
            
        };
    
    //animator = GetComponent<Animator>();


    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
{
   
}
    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Continue()
    {
        pause = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }
    public void Quit()
    {
        Application.Quit();
    }
}
