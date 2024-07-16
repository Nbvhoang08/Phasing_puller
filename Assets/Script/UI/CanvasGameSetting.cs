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
        UIManager.Instance.OpenUI<CanvasMainMenu>();
        this.gameObject.SetActive(false);   
        
    }

}
