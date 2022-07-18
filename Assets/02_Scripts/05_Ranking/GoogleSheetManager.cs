using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GoogleSheetManager : MonoBehaviour
{
    const string URL = "https://script.google.com/macros/s/AKfycbzOxREHm3hzVY79PCRK-UjILs38FqKgbuG6iZo7shAoCu4UNdHD/exec";

    public InputField IDinputField = null;
    public InputField scoreinputField = null;

    string id, score;

    private IEnumerator Start()
    {
        WWWForm form = new WWWForm();
        form.AddField("value", "값");

        UnityWebRequest www = UnityWebRequest.Post(URL, form);
        yield return www.SendWebRequest();

        string _data = www.downloadHandler.text;
        //print(_data);
    }

    bool SetIdPass()
    {
        id = IDinputField.text.Trim();
        score = scoreinputField.text.Trim();

        if (id == "") return false;
        else return true;
    }

    public void Register()
    {
        if (!SetIdPass())
        {
            print("아이디 아니면 비번이 비어있어요\n");
            return;
        }

        WWWForm form = new WWWForm();

        form.AddField("order", "register");
        form.AddField("id", id);
        form.AddField("score", score);

        StartCoroutine(Post(form));
    }

    private IEnumerator Post(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            yield return www.SendWebRequest();

            if (www.isDone) print(www.downloadHandler.text);
            else print("웹의 응답이 없어\n");
        }
    }

}
