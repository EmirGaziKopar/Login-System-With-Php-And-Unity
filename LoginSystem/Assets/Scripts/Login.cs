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
        _URL = _baseURL + "usr="+_usernameInput.text+"&pwd="+_passwordInput.text; //burada php'de yapýlan get iþlemi için
        
        WWW _wb = new WWW(_URL);

        yield return _wb;

        if(_wb.error == null)
        {
            toJson(_wb.text); //json kýsmýnýn görüntülenmesi için _wb içersinde yer alan text'e ukaþmamýz gerek
        }
        else
        {
            Debug.Log("Hata var");
        }
    }


    void toJson(string _json)
    {
        //JSON olarak gelen verilere çevirme iþlemi uygulayacaðýz 
        LoginJSON _loginJSON = JsonUtility.FromJson<LoginJSON>(_json); //FromJson json olan veri tiplerini bool string int gibi verilere çevirirken to json ise o verileri json formatýna çevirir
        if (_loginJSON.success) //buraya giriþ gerçekleþmiyor
        {
            _message.text = "Giriþ Baþarýlý : " + _loginJSON.email;
        }
        else
        {
            _message.text = "Giriþ Baþarýsýz Eror :" + _loginJSON.message;
        }
        
    }

    public void login() //buttona týklandýðýnda bu method çalýþacak
    {
        StartCoroutine(_enumerator()); //enumerator bu þekilde çalýþtýrýlýr
    }
    
}
