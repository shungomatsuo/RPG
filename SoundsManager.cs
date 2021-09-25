using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager instance;
    
    private void Awake()
    {
       if(instance ==null){
          instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
       else
        {
            Destroy(this.gameObject);
        }
    }

    public AudioSource audioSourceBGM;
    public AudioClip[] audioClipBGM;
    public AudioSource audioSourceSE;
    public AudioClip[] audioClipsSE;

    public void StopBGM()
    {
        audioSourceBGM.Stop();
    }

   
    public void PlayBGM(string sceneName)
    {
        audioSourceBGM.Stop();
        switch (sceneName)
        {
         
            default:
            case "Title":
                audioSourceBGM.clip = audioClipBGM[0];
                break;
            case "Town":
                audioSourceBGM.clip = audioClipBGM[1];
                break;
            case "Quest":
                audioSourceBGM.clip = audioClipBGM[2];
                break;
            case "Battle":
                audioSourceBGM.clip = audioClipBGM[3];
                break;
        }
                audioSourceBGM.Play();


        
    }
    public void PlaySE(int index)
    {
        audioSourceSE.PlayOneShot(audioClipsSE[index]);
    }
}
