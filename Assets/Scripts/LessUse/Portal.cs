using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{

    GameObject player_obj;

    // Start is called before the first frame update
    void Start()
    {
        player_obj = GameObject.FindGameObjectWithTag("Player");
        if(player_obj == null) { Debug.Log("null"); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(player_obj.GetComponent<EntityStats>().level_in == 1) { player_obj.GetComponent<EntityStats>().level_in++; }
        
            player_obj.GetComponent<PlayerAttack>().unlock_ultra = 1;
            SaveManager.Instance.SaveGame();
            SceneManager.LoadScene("Title");
        }
        
    }
    
        
    
}
