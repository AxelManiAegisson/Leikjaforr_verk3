using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //togglar end screen off
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Togglar end screen on
    public void ToggleEndMenu(float Score)
    {
        gameObject.SetActive(true);
    }
    //Fer á scene1
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    //Fer á menu scene
    public void ToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

}
