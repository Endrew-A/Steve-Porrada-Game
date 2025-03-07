using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    GameObject player_obj;

    
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Activate());
        player_obj = GameObject.FindGameObjectWithTag("Player");
        //if (player_obj == null) { Debug.Log("null"); }
    }

    //IEnumerator Activate()
    //{
    //    yield return new WaitForSeconds(3);
    //    player_obj = GameObject.FindGameObjectWithTag("Player");
    //    if (player_obj == null) { Debug.Log("null"); }
    //}

    // Update is called once per frame
    void Update()
    {
        float distance = transform.position.x - player_obj.transform.position.x;
        if(distance>=8.5f || distance <= -8.5f)
        {
            player_obj.GetComponent<EntityStats>().coin_qtd++;
            AudioControl.Instance.audios[2].Play();
            UIStats.Instance.CoinsScript();
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player_obj.GetComponent<EntityStats>().coin_qtd++;
            UIStats.Instance.CoinsScript();
            AudioControl.Instance.audios[2].Play();
            Destroy(this.gameObject);
        }
    }
}
