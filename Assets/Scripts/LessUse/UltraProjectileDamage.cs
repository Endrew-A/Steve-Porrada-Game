using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltraProjectileDamage : MonoBehaviour
{
    public float projectile_lifespan;
    public int projectile_dmg;
    public GameObject death_particles;

    public GameObject porrada;
    // Start is called before the first frame update
    void Start()
    {
        porrada = GameObject.Find("Porrada reach sound");
        Destroy(this.gameObject, projectile_lifespan*3);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EntityStats>().RemoveHp(projectile_dmg);
            Instantiate(death_particles, collision.transform.position, Quaternion.identity);
            porrada.GetComponent<AudioSource>().Play();
        }
        else if (collision.gameObject.tag == "Block")
        {
            Instantiate(death_particles, transform.position, Quaternion.identity);
        }
    }
}

