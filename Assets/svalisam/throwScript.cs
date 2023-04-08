using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwScript : MonoBehaviour
{
    public GameObject die;
    public GameObject die1;
    public Vector3 mousePos;
    pauseMenuScript menu;
    public int dice;

    GameInfo info;

    float throwCooldown=0.5f;
    float nextThrow = 0;

    // Start is called before the first frame update
    void Start()
    {
        menu = GameObject.Find("PauseMenu").GetComponent<pauseMenuScript>();
        info = GameObject.Find("GameInfo").GetComponent<GameInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > nextThrow)
        {
            throwDie();
            nextThrow = Time.time + throwCooldown;
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && Time.time > nextThrow)
        {
            throwDie1();
            nextThrow = Time.time + throwCooldown;
        }
    }

    private void FixedUpdate()
    {
        Vector3 tmpPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        mousePos = new Vector3(tmpPos.x - transform.position.x, tmpPos.z - transform.position.y, tmpPos.z - transform.position.z);
            
    }

    public void throwDie()
    {
        if (menu.isPaused)
            return;
        dice--;
        info.addScore();
        if (dice == 0)
            Die();
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;
        if(groundPlane.Raycast(cameraRay,out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.red);
            Instantiate(die, transform.position, Quaternion.identity);
        }

    }

    public void throwDie1()
    {
        if (menu.isPaused)
            return;
        dice--;
        info.addScore();
        if (dice == 0)
            Die();
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;
        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.red);
            Instantiate(die1, transform.position, Quaternion.identity);
        }

    }

    public void Die()
    {
        menu.Lose();
    }

    public void AddDice(int amount)
    {
        dice += amount;
    }


}
