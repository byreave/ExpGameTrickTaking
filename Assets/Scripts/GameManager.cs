using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    public int CurrentCharacterIndex;
    public CharacterScript [] AllCharacters;
    private int BadGuyIndex;
    private Dictionary<string, int> StatusArray;
    public Text[] StatusUI;
    public GameObject Water;

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        StatusArray = new Dictionary<string, int>();
        StatusArray.Add("Food", 100);
        StatusArray.Add("BoatStatus", 100);
        StatusArray.Add("SeaLevel", 100);
        StatusArray.Add("FoodUpkeep", 20);
        StatusArray.Add("BoatStatusUpkeep", 20);
        StatusArray.Add("SeaLevelUpKeep", 20);
        BadGuyIndex = Random.Range(0, 4);
        AllCharacters[BadGuyIndex].isBad = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            Application.Quit();
    }

    public void OnFixClicked()
    {
        if(AllCharacters[CurrentCharacterIndex])
        {
            AllCharacters[CurrentCharacterIndex].TodayJob = 1;
            AllCharacters[CurrentCharacterIndex].JobText.text = "Fix";
            NextCharacter();
        }
    }
    public void OnFishClicked()
    {
        if (AllCharacters[CurrentCharacterIndex])
        {
            AllCharacters[CurrentCharacterIndex].TodayJob = 2;
            AllCharacters[CurrentCharacterIndex].JobText.text = "Fish";
            NextCharacter();
            
        }
    }
    public void OnPrayClicked()
    {
        if (AllCharacters[CurrentCharacterIndex])
        {
            AllCharacters[CurrentCharacterIndex].TodayJob = 3;
            AllCharacters[CurrentCharacterIndex].JobText.text = "Pray";
            NextCharacter();
            
        }
    }

    public void DayEnd()
    {
        foreach (var c in AllCharacters)
        {
            int tmpHelp = 5;
            if (c.isBad)
                tmpHelp = -5;
            if(c.TodayJob == 1)
            {
                StatusArray["BoatStatusUpkeep"] -= tmpHelp;
            }
            else if (c.TodayJob == 2)
            {
                StatusArray["FoodUpkeep"] -= tmpHelp;
            }
            else if (c.TodayJob == 3)
            {
                StatusArray["SeaLevelUpkeep"] -= tmpHelp;
            }
        }
        StatusArray["Food"] -= StatusArray["FoodUpkeep"];
        StatusArray["SeaLevel"] -= StatusArray["SeaLevelUpkeep"];
        StatusArray["BoatStatus"] -= StatusArray["BoatStatusUpkeep"];
        StatusUI[0].text = "Food: " + StatusArray["Food"].ToString() + " / " + StatusArray["FoodUpkeep"];
        StatusUI[1].text = "SeaLevel: " + StatusArray["SeaLevel"].ToString() + " / " + StatusArray["SeaLevelUpkeep"];
        StatusUI[2].text = "BoatStatus: " + StatusArray["BoatStatus"].ToString() + " / " + StatusArray["BoatStatusUpkeep"];
        Water.transform.localScale.Set(Water.transform.localScale.x, StatusArray["SeaLevel"] / 100.0f, Water.transform.localScale.z);
        CurrentCharacterIndex = 0;
        while (AllCharacters[CurrentCharacterIndex].isDead)
            NextCharacter();
    }

    void NextCharacter()
    {
        for(int i = 0; i < AllCharacters.Length; ++ i)
        {
            AllCharacters[i].Hide();
        }
        CurrentCharacterIndex++;
        if (CurrentCharacterIndex == AllCharacters.Length)
            CurrentCharacterIndex = 0;
        while (AllCharacters[CurrentCharacterIndex].isDead)
            CurrentCharacterIndex++;
        if (CurrentCharacterIndex >= AllCharacters.Length)
        {
            DayEnd();
            KillOneCharacter(0);
            DeathCheck();
        }
        else
            AllCharacters[CurrentCharacterIndex].Show();
    }

    void DeathCheck()
    {
        if (StatusArray["Food"] <= 0 || StatusArray["SeaLevel"] <= 0 || StatusArray["BoatStatus"] <= 0)
            SceneManager.LoadScene("FailScene");
        else
        {
            bool success = false;
            for (int i = 0; i < AllCharacters.Length; ++ i)
            {
                if (!AllCharacters[i].isDead && !AllCharacters[i].isBad)
                {
                    success = true;
                    break;
                }
            }
            if(!success)
                SceneManager.LoadScene("FailScene");
        }
    }

    void KillOneCharacter(int ChaIndex)
    {
        if(ChaIndex == BadGuyIndex)
        {
            SceneManager.LoadScene("WinScene");
        }
        else
        {
            AllCharacters[ChaIndex].isDead = true;
            AllCharacters[ChaIndex].JobText.text = "Dead";
            
        }
    }
}
