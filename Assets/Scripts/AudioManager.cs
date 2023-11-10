using System.Collections.Generic;
using UnityEngine;

public enum SfxType
{
    Attack,
    GiveDamage,
    TakeDamag
}

public enum MusicType
{
    MainMenu,
    Game
}

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSourceSfx;
    [SerializeField] private AudioSource _audioSourceMusic;

    [SerializeField] private List<SfxData> _sfxDatas = new List<SfxData>();

    [SerializeField] private List<MusicData> _musicDatas = new List<MusicData>();

    public static AudioManager Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayMusic(MusicType type)
    {
        var musicData = GetMusic(type);
        _audioSourceMusic.clip = musicData.Music;
        _audioSourceMusic.Play();
    }

    public void PlaySfx(ElementType elementType, SfxType sfxType)
    {
        var sfxData = GetSfx(elementType, sfxType);
        _audioSourceSfx.PlayOneShot(sfxData.AudioClip);
    }

    private Sfx GetSfx(ElementType elementType, SfxType sfxType)
    {
        var result = _sfxDatas.Find(sfxData => sfxData.SfxType == sfxType).Sfxes;
        // sfxData.ElementType == elementType
        return result.Find(sfxData => sfxData.ElementType == elementType);
    }

    private MusicData GetMusic(MusicType type)
    {
        var result = _musicDatas.Find(musicData => musicData.Type == type);
        return result;
    }
}

[System.Serializable]
public class SfxData
{
    [field: SerializeField] public SfxType SfxType { get; private set; }
    [field: SerializeField] public List<Sfx> Sfxes { get; private set; }

}

[System.Serializable]
public class Sfx
{
    [field: SerializeField] public ElementType ElementType { get; private set; }
    [field: SerializeField] public AudioClip AudioClip { get; private set; }
}

[System.Serializable]
public class MusicData
{
    [field: SerializeField] public MusicType Type { get; private set; }
    [field: SerializeField] public AudioClip Music { get; private set; }
}