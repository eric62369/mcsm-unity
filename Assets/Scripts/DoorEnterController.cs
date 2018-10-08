using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class also handles User Input!
/// Handles when the player tries to enter a door
/// </summary>
public class DoorEnterController : MonoBehaviour {

    public string NextLevel;

    public bool playerInTrigger;

    private void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.W))
        {
            GoToNextRoom();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            playerInTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            playerInTrigger = false;
        }
    }
    public void GoToNextRoom()
    {
        SceneManager.LoadScene(NextLevel);
    }
}
