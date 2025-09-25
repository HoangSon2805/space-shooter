using UnityEngine;

[DisallowMultipleComponent]
public class AudioManager : MonoBehaviour {
    public static AudioManager I;

    public int voices = 8;

    // --- SFX (mỗi loại 1 ô kéo AudioClip) ---
    public AudioClip shoot; public float shootVol = 1f; public Vector2 shootPitch = new(0.98f, 1.02f);
    public AudioClip hit; public float hitVol = 0.9f; public Vector2 hitPitch = new(0.95f, 1.05f);
    public AudioClip enemyDie; public float enemyDieVol = 1f; public Vector2 enemyDiePitch = new(0.98f, 1.02f);
    public AudioClip levelUp; public float levelUpVol = 1f; public Vector2 levelUpPitch = new(1f, 1f);

    // --- Nhạc nền (tuỳ chọn) ---
    public AudioClip musicClip; public float musicVolume = 0.6f;

    AudioSource[] _pool; int _idx; AudioSource _music;

    void Awake() {
        if (I != null) { Destroy(gameObject); return; }
        I = this; DontDestroyOnLoad(gameObject);

        // Pool SFX
        int n = Mathf.Max(1, voices);
        _pool = new AudioSource[n];
        for (int i = 0; i < n; i++)
        {
            var a = gameObject.AddComponent<AudioSource>();
            a.playOnAwake = false; a.loop = false; a.spatialBlend = 0f;
            _pool[i] = a;
        }

        // Music
        _music = gameObject.AddComponent<AudioSource>();
        _music.playOnAwake = false; _music.loop = true; _music.spatialBlend = 0f;

        if (musicClip) { _music.clip = musicClip; _music.volume = musicVolume; _music.Play(); }
    }

    AudioSource Next() { var a = _pool[_idx]; _idx = (_idx + 1) % _pool.Length; return a; }

    void PlayOne(AudioClip clip, float vol, Vector2 pitch) {
        if (!clip) return;
        var a = Next();
        a.pitch = Random.Range(pitch.x, pitch.y);
        a.PlayOneShot(clip, Mathf.Clamp01(vol));
    }

    // API gọi từ gameplay
    public void SfxShoot() => PlayOne(shoot, shootVol, shootPitch);
    public void SfxHit() => PlayOne(hit, hitVol, hitPitch);
    public void SfxEnemyDie() => PlayOne(enemyDie, enemyDieVol, enemyDiePitch);
    public void SfxLevelUp() => PlayOne(levelUp, levelUpVol, levelUpPitch);

    public void PlayMusic(AudioClip clip, float volume = 0.6f) {
        if (!clip) return;
        _music.clip = clip; _music.volume = Mathf.Clamp01(volume); _music.Play();
    }
}
