using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;


public enum FxID
{
    Click
}
public class SoundManager : Singleton<SoundManager>
{
    
    public PlayerSoundData soundData;
 /*   public AudioSource SfxSource;*/
    public AudioSource MusicSource;

    public AudioMixerGroup[] AudioMixerGroups;
    public AudioMixer mixer;
    public int index = 0;
    public Slider music;
    /*public Slider sfx;
    public Slider master;*/


    protected override void Awake()
    {
        base.Awake();

        MusicSource = gameObject.AddComponent<AudioSource>();
        MusicSource.loop = true;
        MusicSource.outputAudioMixerGroup = AudioMixerGroups[1];
        
        
      

    /*  SfxSource = gameObject.AddComponent<AudioSource>();
        SfxSource.outputAudioMixerGroup = AudioMixerGroups[2];
        SfxSource.loop = false;*/

    }

    public void Start()
    {
        ChangeMusic();
        PlayMusic(true);
    }
    public void ChangeMusic()
    {
        MusicSource.clip = soundData.BackGroundMusic[index];
    }

    private void Update()
    {
        ChangeMusic();
    }


    public void PlayMusic(bool play)
    {
        if (play)
        {
            MusicSource.Play();
        }
        else
        {
            MusicSource.Stop();
        }
    }
    /* public void PlayFx(FxID ID)
     {
         SfxSource.PlayOneShot(soundData.Sfx[(int)ID]);
     }*/

    /*  public void PlayFxClicked()
      {
          PlayFx(FxID.Click);
      }*/

    public void ValueChangeInSlider()
    {
        mixer.SetFloat("Music", Mathf.Log10(music.value) * 20);
    
        /* mixer.SetFloat("SFX", Mathf.Log10(sfx.value) * 20);*/
        /* mixer.SetFloat("SFX", Mathf.Log10(sfx.value) * 20);*/
    }

}
