/*//using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Text;

public class VoiceRecognition : MonoBehaviour
{
    private DictationRecognizer dictationRecognizer; 
    private StringBuilder recognizedText;           

    void Start()
    {
        recognizedText = new StringBuilder();

        dictationRecognizer = new DictationRecognizer();

        dictationRecognizer.DictationResult += (text, confidence) =>
        {
            Debug.Log("Text recunoscut: " + text);
            recognizedText.Append(text + " ");
        };

        dictationRecognizer.DictationError += (error, hresult) =>
        {
            Debug.LogError("Eroare la recunoastere: " + error);
        };

        dictationRecognizer.Start();
        Debug.Log("Recunoasterea vocala a pornit.");
    }

    void OnDestroy()
    {
        if (dictationRecognizer != null && dictationRecognizer.Status == SpeechSystemStatus.Running)
        {
            dictationRecognizer.Stop();
            dictationRecognizer.Dispose();
        }
    }
}
*/

using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Text;
using UnityEngine.UI;  // Pentru a folosi UI Text
using TMPro;

public class VoiceRecognition : MonoBehaviour
{
    private DictationRecognizer dictationRecognizer;
    private StringBuilder recognizedText;
    private float startTime;
    private int wordCount = 0;
    public TMP_Text wpmText;  

    void Start()
    {
        recognizedText = new StringBuilder();

       /* wpmText = GameObject.Find("Canva").GetComponent<Text>();*/

        dictationRecognizer = new DictationRecognizer();

        dictationRecognizer.DictationResult += (text, confidence) =>
        {
            recognizedText.Append(text + " ");
            wordCount += CountWords(text); 
        };

        dictationRecognizer.DictationError += (error, hresult) =>
        {
            Debug.LogError("Eroare la recunoastere: " + error);
        };

        dictationRecognizer.Start();
        startTime = Time.time;  
        Debug.Log("Recunoasterea vocala a pornit.");
    }

    void Update()
    {
        float timePassed = Time.time - startTime;
        float wpm = (wordCount / timePassed) * 60; 

        wpmText.text = "WPM: " + Mathf.RoundToInt(wpm).ToString();

        if (wpm < 125)
        {
            wpmText.text += "\nVorbesti prea incet!";
        }
        else if (wpm > 160)
        {
            wpmText.text += "\nVorbesti prea repede!";
        }
    }

    int CountWords(string sentence)
    {
        string[] words = sentence.Split(' ');
        return words.Length;
    }

    void OnDestroy()
    {
        if (dictationRecognizer != null && dictationRecognizer.Status == SpeechSystemStatus.Running)
        {
            dictationRecognizer.Stop();
            dictationRecognizer.Dispose();
        }
    }
}
