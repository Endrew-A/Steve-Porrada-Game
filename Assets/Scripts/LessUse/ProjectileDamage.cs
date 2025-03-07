using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public float projectile_lifespan;
    public int projectile_dmg;
    public GameObject death_particles;
    public bool is_player;

    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, projectile_lifespan);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Player" && !is_player) || (collision.gameObject.tag == "Enemy" && is_player))
        {
            collision.gameObject.GetComponent<EntityStats>().RemoveHp(projectile_dmg);
            Destroy(this.gameObject);
            Instantiate(death_particles, transform.position, Quaternion.identity);
        }
        else if(collision.gameObject.tag == "Block")
        {
            Destroy(this.gameObject);
            Instantiate(death_particles, transform.position, Quaternion.identity);
        }

        if(collision.gameObject.tag == "Enemy" && is_player)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>().porrada.Play();
        }else if(collision.gameObject.tag == "Player" && !is_player) { GameObject.FindGameObjectWithTag("Enemy").GetComponent<ProjectileShoot>().hit.Play(); }
    }
}
