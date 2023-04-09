using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tutorialScript : MonoBehaviour
{
    public GameObject screen1;
    public GameObject screen2;
    public GameObject screen3;
    // Start is called before the first frame update
    void Start()
    {
        screen2.SetActive(false);
        screen3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void firstStep()
    {
        screen1.SetActive(false);
        screen2.SetActive(true);
    }

    public void secondStep()
    {
        screen2.SetActive(false);
        screen3.SetActive(true);
    }

    public void thirdStep()
    {
        screen3.SetActive(false);
    }

    public void Play(){
        SceneManager.LoadScene(2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            Play();
    }
}
