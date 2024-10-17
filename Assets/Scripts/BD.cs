using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;
using TMPro;
using Newtonsoft.Json;
using Firebase.Database;

public class BD : MonoBehaviour
{
    public DatabaseReference reference;
    [SerializeField] TMP_InputField textoNombre;
    [SerializeField] TMP_InputField textoNumero;
    public bool booleano = true;
    public string dato;


    private void Awake()
    {
        DontDestroyOnLoad(this);
        reference = FirebaseDatabase.DefaultInstance.RootReference;

    }

    public void Booleano(bool toggle)
    {
        booleano = toggle;
    }

    public void Registro()
    {
        //Guardar los datos en un nuevo registro de nombre

        reference.Child("Nombre").SetValueAsync(textoNombre.text);

        //Guardar los datos en un nuevo registro de numero

        reference.Child("Numero").SetValueAsync(int.Parse(textoNumero.text));

        //Guardar los datos en un nuevo registro de bool

        reference.Child("Booleano").SetValueAsync(booleano);

        //Guardar datos con clave unica
        string Key = reference.Child("Nombre").Push().Key;

        //Guarda datos en la clave

        reference.Child("Nombre").Child(Key).SetValueAsync(textoNombre.text);

        //Guardar archivo Json
        Usuario usuarioN = new Usuario("NombreUusario", "Correo@algo.com", "sdfghj2s");
        string json =  JsonUtility.ToJson(usuarioN);

        reference.Child("Usuarios").SetRawJsonValueAsync(json);

        LoadFromBD();

    }

    public void LoadFromBD()
    {
        reference.Child("Numero").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Error" + task.Exception);
            }

            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                if (snapshot.Exists)
                {
                    string value = snapshot.Value.ToString();
                    Debug.Log("Tipo de Valor" + snapshot.Value.GetType());
                    Debug.Log(value);
                }
                else
                {
                    Debug.Log("Registro no encontrado");
                }
            }
        });
            
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}

public class Usuario
{
    public string userName;
    public string email;
    public string password;
    public Usuario(string userName, string email, string password)
    {
        this.userName = userName;
        this.email = email;     
        this.password = password;
    }
}
