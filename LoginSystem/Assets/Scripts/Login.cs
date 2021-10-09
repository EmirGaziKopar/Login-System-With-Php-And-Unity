using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Web;
using UnityEngine.UI;

public class Login : MonoBehaviour
{

    public string _baseURL = "http://localhost/game/login.php?";
    public string _URL;
    public InputField _usernameInput;
    public InputField _passwordInput;
    public Text _message;

    // Start is called before the first frame update
    void Start()
    {
        login();
    }

    IEnumerator _enumerator()
    {
        _URL = _baseURL + "usr="+_usernameInput.text+"&pwd="+_passwordInput.text; //burada php'de yap�lan get i�lemi i�in
        
        WWW _wb = new WWW(_URL);

        yield return _wb;

        if(_wb.error == null)
        {
            toJson(_wb.text); //json k�sm�n�n g�r�nt�lenmesi i�in _wb i�ersinde yer alan text'e uka�mam�z gerek
        }
        else
        {
            Debug.Log("Hata var");
        }
    }


    void toJson(string _json)
    {
        //JSON olarak gelen verilere �evirme i�lemi uygulayaca��z 
        LoginJSON _loginJSON = JsonUtility.FromJson<LoginJSON>(_json); //FromJson json olan veri tiplerini bool string int gibi verilere �evirirken to json ise o verileri json format�na �evirir
        if (_loginJSON.success) //buraya giri� ger�ekle�miyor
        {
            _message.text = "Giri� Ba�ar�l� : " + _loginJSON.email;
        }
        else
        {
            _message.text = "Giri� Ba�ar�s�z Eror :" + _loginJSON.message;
        }
        
    }

    public void login() //buttona t�kland���nda bu method �al��acak
    {
        StartCoroutine(_enumerator()); //enumerator bu �ekilde �al��t�r�l�r
    }
    
}
