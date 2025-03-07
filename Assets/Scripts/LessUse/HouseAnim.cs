using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseAnim : MonoBehaviour
{

    public GameObject player_anim, player;
    float waitTime = 3f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Activate());
    }
    IEnumerator Activate()
    {
        yield return new WaitForSeconds(waitTime);
        player.SetActive(true);
        player_anim.SetActive(false);
    }
 
}
