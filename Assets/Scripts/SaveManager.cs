using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }


    public GameObject player_obj;

    private void Awake()

    {

        if (Instance != null && Instance != this)

        {

            Destroy(this);

        }

        else

        {

            Instance = this;

        }


    }
    // Start is called before the first frame update
    void Start()
    {
        AudioControl.Instance.ApplyAudio();
        LoadGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveGame()
    {
        //STATS
        PlayerPrefs.SetInt("hp", player_obj.GetComponent<EntityStats>().max_hp);
        PlayerPrefs.SetInt("attack_dmg", player_obj.GetComponent<EntityStats>().attack_dmg);
        PlayerPrefs.SetFloat("attack_speed", player_obj.GetComponent<EntityStats>().attack_speed);
        PlayerPrefs.SetFloat("attack_range", player_obj.GetComponent<EntityStats>().attack_range);

        PlayerPrefs.SetInt("unlock_ultra", player_obj.GetComponent<PlayerAttack>().unlock_ultra);
        PlayerPrefs.SetInt("level_in", player_obj.GetComponent<EntityStats>().level_in);

        PlayerPrefs.SetInt("coin_qtd", player_obj.GetComponent<EntityStats>().coin_qtd);
        //PlayerPrefs.SetInt("coin_qtd", 0);
    }

    public void SaveAudio()
    {
        PlayerPrefs.SetFloat("volume", UI.Instance.volume_slider.value);
    }
    public void LoadAudio()
    {
        UI.Instance.volume_slider.value = PlayerPrefs.GetFloat("volume");
    }

    public void LoadGame()
    {
        player_obj.GetComponent<EntityStats>().max_hp = PlayerPrefs.GetInt("hp");
        if(player_obj.GetComponent<EntityStats>().max_hp == 1) { player_obj.GetComponent<EntityStats>().max_hp = 4; }
        player_obj.GetComponent<EntityStats>().attack_dmg = PlayerPrefs.GetInt("attack_dmg");
        if (player_obj.GetComponent<EntityStats>().attack_dmg == 0) { player_obj.GetComponent<EntityStats>().attack_dmg = 2; }
        player_obj.GetComponent<EntityStats>().attack_speed = PlayerPrefs.GetFloat("attack_speed");
        if (player_obj.GetComponent<EntityStats>().attack_speed == 0) { player_obj.GetComponent<EntityStats>().attack_speed = 2; }
        player_obj.GetComponent<EntityStats>().attack_range = PlayerPrefs.GetFloat("attack_range");
        if (player_obj.GetComponent<EntityStats>().attack_range == 0) { player_obj.GetComponent<EntityStats>().attack_range = 7; SaveGame(); }

        player_obj.GetComponent<PlayerAttack>().unlock_ultra = PlayerPrefs.GetInt("unlock_ultra");
        if(player_obj.GetComponent<PlayerAttack>().unlock_ultra == 0) { player_obj.GetComponent<PlayerAttack>().unlock_ultra = 0; }
        player_obj.GetComponent<EntityStats>().level_in = PlayerPrefs.GetInt("level_in");

        player_obj.GetComponent<EntityStats>().coin_qtd = PlayerPrefs.GetInt("coin_qtd");
        if (player_obj.GetComponent<EntityStats>().coin_qtd == 0) { player_obj.GetComponent<EntityStats>().coin_qtd = 0; }
        //player_obj.GetComponent<EntityStats>().coin_qtd = 0;
    }
}
