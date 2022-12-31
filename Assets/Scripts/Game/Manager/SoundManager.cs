using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	void Start()
	{
		BuildAudioLibrary();
	}

	void BuildAudioLibrary()
	{
		Audios.Clear();

		var sources = GetComponentsInChildren<AudioSource>();
		foreach (var source in sources)
		{
			var clipName = source.clip.name;
			if (!Audios.ContainsKey(clipName))
			{
				Audios.Add(clipName, source);
			}

			if (!AudioVolumeCache.ContainsKey(clipName))
			{
				AudioVolumeCache.Add(clipName, source.volume);
			}
		}
	}

	static Dictionary<string, AudioSource> Audios => audios ?? (audios = new Dictionary<string, AudioSource>());
	static Dictionary<string, AudioSource> audios;

	static Dictionary<string, float> AudioVolumeCache => audioVolumeCache ?? (audioVolumeCache = new Dictionary<string, float>());
	static Dictionary<string, float> audioVolumeCache;

	public static void PlayAudio(string clipName)
	{
		if (Audios.ContainsKey(clipName))
		{
			Audios[clipName].Play();
		}
	}

	public static void SetMasterVolume(float volumePercentage)
	{
		foreach (var audio in Audios)
		{
			if (AudioVolumeCache.ContainsKey(audio.Key))
			{
				audio.Value.volume = AudioVolumeCache[audio.Key] * volumePercentage;
			}
		}
	}
}
