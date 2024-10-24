using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase.Extensions;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BDYugiOH : MonoBehaviour
{
    [SerializeField] TMP_InputField cardNameInput;
    [SerializeField] TMP_InputField attackInput;
    [SerializeField] TMP_InputField defenseInput;
    [SerializeField] TMP_InputField spellTrapInput;
    [SerializeField] Toggle isMonsterToggle;
    [SerializeField] BDYugiOH yugiOHCards;

    public bool isMonster = true;
    public string cardName;
    public int attack;
    public int defense;
    public string spellTrap;
    public DatabaseReference reference;

    public void Awake()
    {
        DontDestroyOnLoad(this);
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }
    public void Start()
    {
        cardNameInput.text = "";
        attackInput.text = "";
        defenseInput.text = "";
        spellTrapInput.text = "";
        isMonsterToggle.isOn = true;
        spellTrapInput.gameObject.SetActive(false);
        attackInput.gameObject.SetActive(true);
        defenseInput.gameObject.SetActive(true);



    }

    public void Booleano(bool toggle)
    {
        if (toggle)
        {
            spellTrapInput.gameObject.SetActive(false);
            attackInput.gameObject.SetActive(true);
            defenseInput.gameObject.SetActive(true);
        }
        else
        {
            spellTrapInput.gameObject.SetActive(true);
            attackInput.gameObject.SetActive(false);
            defenseInput.gameObject.SetActive(false);



        }
    }
    
    
    public void RegisterCard()
    {
        cardName = cardNameInput.text;
        isMonster = isMonsterToggle.isOn;

        if (string.IsNullOrEmpty(cardName))
        {
            Debug.LogError("Nombre necesario");
            return;
        }

        Dictionary<string, object> cardData = new Dictionary<string, object>();
        cardData["TipoMonstruo"] = isMonster;

        if (isMonster)
        {
            attack = int.Parse(attackInput.text);
            defense = int.Parse(defenseInput.text);

            cardData["ATK"] = attack;
            cardData["DEF"] = defense;
         
        }

        else
        {
            spellTrap = spellTrapInput.text;
            cardData["Tipo"] = spellTrap;
        }


        reference.Child("Cartas").Child(cardName).SetValueAsync(cardData).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Carta Registrada"+cardName);
                yugiOHCards.Start();
            }
            else
            {
                Debug.LogError("Error" + task.Exception);
            }


        });
    }

    public void LoadARScene()
    {
        SceneManager.LoadScene("P2");
    }


    public void LodCardData(string cardNameAR, TMP_Text textMesh)
    {
        reference.Child("Cartas").Child(cardNameAR).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            DataSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                bool isMonster = bool.Parse(snapshot.Child("TipoMonstruo").Value.ToString());
                string displayInfo;
                if (isMonster)
                {
                    int attack = int.Parse(snapshot.Child("ATK").Value.ToString());
                    int defense = int.Parse(snapshot.Child("DEF").Value.ToString());

                    displayInfo = $"Monstruo\nATK:{attack}\nDEF:{defense}";

                }
                else
                {
                    string type = snapshot.Child("Tipo").Value.ToString();
                    displayInfo = $"Magia/Trampa\nType:{type}"; 
                }

                textMesh.text = displayInfo;
            }
            else
            {
                textMesh.text = "Error";
            }
        });
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
