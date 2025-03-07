using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI Instance { get; private set; }
    public GameObject game_over;
    public GameObject redscreen;
    public GameObject pause_scene;

    public Slider volume_slider;
    public Text volume_text;
    public bool in_options = false,istitle;
    public GameObject options, title, levelslect, stats;
    public Text subtitle;

    public Text hp, dmg, atck_speed, range;
    public Text coin_qtd;
    public GameObject red_x;
    public Text ultra_text;

    //public List<AudioSource> audios;
    public AudioSource click;

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
        if (istitle)
        {
            int num = Random.Range(1, 11);
            if (num == 10) { subtitle.text = "Hello world!"; }
            else if (num == 5) { subtitle.text = "Steve is not herobrine!"; }
            else if (num == 7) { subtitle.text = "Palmeiras bigger of Brazil!"; }
            else { subtitle.text = "Also try being a good game!"; }
            istitle = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (in_options) { volume_text.text = Mathf.CeilToInt(volume_slider.value * 100) + "%"; }
    }

    public void EnabledGameOver()
    {
        game_over.SetActive(true);
        redscreen.SetActive(true);
    }

    public void Restart()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        
        SceneManager.LoadScene(currentScene);
    }

    public void TitleScreen()
    {
        click.Play();
        SceneManager.LoadScene("Title");
    }

    public void QuitGame()
    {
        click.Play();
        Application.Quit();
    }

    public void StartGame()
    {
        //SceneManager.LoadScene("Fase1");
        title.SetActive(false);
        click.Play();
        levelslect.SetActive(true);
    }

    //public void StartLevel()
    //{
    //    int num;

    //    SceneManager.LoadScene("Fase" + num);
    //}

    public void Button(int btnum)
    {
        click.Play();
        if (btnum == 1) { SceneManager.LoadScene("Fase1"); }
        //SaveManager.Instance.LoadGame();
    }
    public void MenuButton()
    {      
        SaveManager.Instance.SaveAudio();
        AudioControl.Instance.ApplyAudio();
        click.Play();
        SceneManager.LoadScene("Title");
        istitle = true;
        Time.timeScale = 1;
    }
    public void Options()
    {
        click.Play();
        title.SetActive(false);
        options.SetActive(true);
        SaveManager.Instance.LoadAudio();
        in_options = true;
    }

    public void Back()
    {
        //foreach(AudioSource audio in audios)
        //{
        //    audio.volume = volume_slider.value;
        //}
        click.Play();
        SaveManager.Instance.SaveAudio();
        AudioControl.Instance.ApplyAudio();
        title.SetActive(true);
        options.SetActive(false);
        in_options = false;
    }

    public void Pause()
    {
        SaveManager.Instance.LoadAudio();
        in_options = true;
        Time.timeScale = 0;
        pause_scene.SetActive(true);
    }

    public void BackPause()
    {
        SaveManager.Instance.SaveAudio();
        AudioControl.Instance.ApplyAudio();
        click.Play();
        pause_scene.SetActive(false);
        in_options = false;
        Time.timeScale = 1;
    }

    public void BackTitle()
    {
        GameObject currentgmobj = GameObject.FindGameObjectWithTag("Scene");
        click.Play();
        currentgmobj.SetActive(false);
        title.SetActive(true);
    }

    public void StatsScreen()
    {
        click.Play();
        LevelUpScript.Instance.in_stats = true;
        stats.SetActive(true);
        levelslect.SetActive(false);
        //SaveManager.Instance.LoadGame();
        RenewStats();

        int unlock_ultra = PlayerPrefs.GetInt("unlock_ultra");
        if(unlock_ultra == 0)
        {
            red_x.SetActive(true);
            ultra_text.text = "XXXXXXXXXX";
        }
        else
        {
            red_x.SetActive(false);
            ultra_text.text = "Ultra Porrada";
        }
    }
    public void RenewStats()
    {
        //Primeira vez abrindo o jogo
        //int intteste;
        //intteste = PlayerPrefs.GetInt("hp"); if(intteste ==0)

        int hp_ = PlayerPrefs.GetInt("hp");
        if(hp_ == 0 || hp_ == 1) { PlayerPrefs.SetInt("hp", 4); }

        //
        hp.text = "HP - " + PlayerPrefs.GetInt("hp").ToString();
        dmg.text = "Damage - " + PlayerPrefs.GetInt("attack_dmg").ToString();
        atck_speed.text = "Attack Speed - " + PlayerPrefs.GetFloat("attack_speed").ToString();
        range.text = "Range - " + PlayerPrefs.GetFloat("attack_range").ToString();
        coin_qtd.text = PlayerPrefs.GetInt("coin_qtd").ToString();
    }
}
