using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    public float base_move, attack_speed, attack_range,attack_lifespan;
    public int hp, max_hp, attack_dmg, exp, level=1;
    public GameObject deathanim;
    public bool isboss_ = false;
    public GameObject portalanim;
    public GameObject coin;
    public int coin_qtd;

    //PLAYER
    public int level_in=1;

    // Start is called before the first frame update
    void Start()
    {
        //if(gameObject.tag == "Enemy") { hp = max_hp; }
        RenewHp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveHp(int dmg)
    {
        hp -= dmg;
        Death();
    }

    void Death()
    {
        if (hp <= 0)
        {
            if(gameObject.tag == "Enemy") { GameObject.FindGameObjectWithTag("Player").GetComponent<EntityStats>().Addexp(exp); 
                if(isboss_ == true)
                {
                    deathanim.SetActive(true);
                    deathanim.GetComponent<Animator>().Play("death");

                    portalanim.SetActive(true);
                }
                for (int i = 0; i < coin_qtd; i++)
                {
                    GameObject coin_jump = Instantiate(coin, this.gameObject.transform.position, Quaternion.identity);
                    coin_jump.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-3f, 3f), 2), ForceMode2D.Impulse);
                }
                

            }
            Destroy(this.gameObject);
            if (gameObject.tag == "Player") { UI.Instance.EnabledGameOver(); gameObject.GetComponent<PlayerMovement>().footsteps.volume = 0;
                GameObject.Find("Soundtrack").GetComponent<AudioSource>().Stop(); }           
        }
    }

    public void RenewHp()
    {
        hp = max_hp;
    }

    void Addexp(int exp_)
    {
        exp += exp_;
        if(exp >= level * 50)
        {
            level++;
            exp = 0;
        }
    }
}
