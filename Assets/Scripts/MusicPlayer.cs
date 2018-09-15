using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
    private AudioSource[] versions;

    public float fadeTime;

    private float fadeAmount;

    private List<MusicVolume> volumes;

    public static MusicPlayer inst { get; private set; }

    private void Awake() {
        inst = this;
        versions = GetComponents<AudioSource>();
        UpdatePlaying();
    }

    private int fadingOut = -1;
    private int _currentVersion = -1;
    private int currentVersion {
        get { return _currentVersion; }
        set {
            if (_currentVersion != value) {
                if (fadingOut >= 0) {
                    versions[fadingOut].Stop();
                }

                fadingOut = currentVersion;
                _currentVersion = value;

                if (_currentVersion >= 0) {
                    versions[_currentVersion].volume = 0;
                    versions[_currentVersion].Play();
                }
            }
        }
    }

    public void Enter(MusicVolume volume) {
        if (!volumes.Contains(volume)) {
            volumes.Add(volume);
            UpdatePlaying();
        }
    }

    public void Leave(MusicVolume volume) {
        volumes.Remove(volume);
        UpdatePlaying();
    }

	void UpdatePlaying() {
        currentVersion = volumes.Count > 0 ? volumes[0].version : 0;
    }

    private void Update() {
        if (fadingOut >= 0) {
            var fo = versions[fadingOut];
            fo.volume = Mathf.MoveTowards(fo.volume, 0, Time.deltaTime / fadeTime);
            if (fo.volume == 0) {
                fo.Stop();
            }
        }

        if (currentVersion >= 0) {
            var cv = versions[currentVersion];
            cv.volume = Mathf.MoveTowards(cv.volume, 1, Time.deltaTime / fadeTime);
        }
    }
}
