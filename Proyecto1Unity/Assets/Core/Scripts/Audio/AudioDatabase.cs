using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio/Audio Manager")]
public class AudioDatabase : ScriptableObject
{
    [Header("Test Sounds AudioClips")]
    public AudioClip testSoundAC;
    [Range(0, 1)] public float testSoundVolume;
    [NonSerialized] public float testSoundCurrentVolume;

    [Header("Test Dialogues AudioClips")]
    public AudioClip testDialogueAC;
    [Range(0, 1)] public float testDialogueVolume;
    [NonSerialized] public float testDialogueCurrentVolume;

    [Header("Test Music AudioClips")]
    public AudioClip testMusicAC;
    [Range(0, 1)] public float testMusicVolume;
    [NonSerialized] public float testMusicCurrentVolume;
}
