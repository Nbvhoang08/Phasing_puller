using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGameSetting : UICanvas
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LeftButton()
    {
        SoundManager.Instance.PlayFxClicked();
        if (SoundManager.Instance.soundData.BackGroundMusic.Count > 0)
        {
            SoundManager.Instance.index--;
            if (SoundManager.Instance.index < 0)
            {
                SoundManager.Instance.index = SoundManager.Instance.soundData.BackGroundMusic.Count - 1;
            }
            SoundManager.Instance.ChangeMusic();
            SoundManager.Instance.MusicSource.Play();
        }
    }
    public void RightButton()
    {
        SoundManager.Instance.PlayFxClicked();
        if (SoundManager.Instance.soundData.BackGroundMusic.Count > 0)
        {
            SoundManager.Instance.index++;
            if (SoundManager.Instance.index > SoundManager.Instance.soundData.BackGroundMusic.Count)
            {
                SoundManager.Instance.index = 0;
            }
            SoundManager.Instance.ChangeMusic();
            SoundManager.Instance.MusicSource.Play();
        }
    }
    public void BackButton()
    {
        SoundManager.Instance.PlayFxClicked();
        UIManager.Instance.OpenUI<CanvasMainMenu>();
        this.gameObject.SetActive(false);   
        
    }

}
