﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio/Audio Manager")]
public class AudioDatabase : ScriptableObject
{
    [Header("Bufo AudioClips")]

        public AudioClip bufoTongueAC;
         [Range(0, 1)] public float bufoTongueVolume;
         [NonSerialized] public float bufoTongueCurrentVolume;

        public AudioClip bufoJumpAC;
         [Range(0, 1)] public float bufoJumpVolume;
         [NonSerialized] public float bufoJumpCurrentVolume;

        public AudioClip bufoTongueImpAC;
         [Range(0, 1)] public float bufoTongueImpVolume;
         [NonSerialized] public float bufoTongueImpCurrentVolume;

        public AudioClip bufoFlutterRightAC;
         [Range(0, 1)] public float bufoFlutterRightVolume;
         [NonSerialized] public float bufoFlutterRightCurrentVolume;

        public AudioClip bufoFlutterLeftAC;
         [Range(0, 1)] public float bufoFlutterLeftVolume;
         [NonSerialized] public float bufoFlutterLeftCurrentVolume;

        public AudioClip bufoShiftAirAC;
         [Range(0, 1)] public float bufoShiftAirVolume;
         [NonSerialized] public float bufoShiftAirCurrentVolume;

        public AudioClip bufoStepsRightAC;
         [Range(0, 1)] public float bufoStepsRightVolume;
         [NonSerialized] public float bufoStepsRightCurrentVolume;

        public AudioClip bufoStepsLeftAC;
         [Range(0, 1)] public float bufoStepsLeftVolume;
         [NonSerialized] public float bufoStepsLeftCurrentVolume;

        public AudioClip bufoLandingAC;
         [Range(0, 1)] public float bufoLandingVolume;
         [NonSerialized] public float bufoLandingCurrentVolume;

        public AudioClip bufoDeathAC;
         [Range(0, 1)] public float bufoDeathVolume;
         [NonSerialized] public float bufoDeathCurrentVolume;

        public AudioClip bufoBiteAC;
         [Range(0, 1)] public float bufoBiteVolume;
         [NonSerialized] public float bufoBiteCurrentVolume;

        public AudioClip bufoHurtAC;
         [Range(0, 1)] public float bufoHurtVolume;
         [NonSerialized] public float bufoHurtCurrentVolume;

    [Header("Interactive Elements AudioClips")]

        public AudioClip pressurePlateAC;
         [Range(0, 1)] public float pressurePlateVolume;
         [NonSerialized] public float pressurePlateCurrentVolume;

        public AudioClip pressurePlateBrokenAC;
         [Range(0, 1)] public float pressurePlateBrokenVolume;
         [NonSerialized] public float pressurePlateBrokenCurrentVolume;

        public AudioClip leverAC;
         [Range(0, 1)] public float leverVolume;
         [NonSerialized] public float leverCurrentVolume;

        public AudioClip boxShiftAC;
         [Range(0, 1)] public float boxShiftVolume;
         [NonSerialized] public float boxShiftCurrentVolume;

        public AudioClip collectableAC;
         [Range(0, 1)] public float collectableVolume;
         [NonSerialized] public float collectableCurrentVolume;

        public AudioClip timeTrialAC;
         [Range(0, 1)] public float timeTrialVolume;
         [NonSerialized] public float timeTrialCurrentVolume;

        [Header("Cobra AudioClips")]

             public AudioClip cobraThrowingBrickAC;
              [Range(0, 1)] public float cobraThrowingBrickVolume;
              [NonSerialized] public float cobraThrowingBrickCurrentVolume;

             public AudioClip cobraFleeAC;
              [Range(0, 1)] public float cobraFleeVolume;
              [NonSerialized] public float cobraFleeCurrentVolume;

             public AudioClip cobraDetectionAC;
              [Range(0, 1)] public float cobraDetectionVolume;
              [NonSerialized] public float cobraDetectionCurrentVolume;

             public AudioClip cobraDeathAC;
             [Range(0, 1)] public float cobraDeathVolume;
             [NonSerialized] public float cobraDeathCurrentVolume;

    [Header("Mosquito AudioClips")]

            public AudioClip mosquitoHumAC;
             [Range(0, 1)] public float mosquitoHumVolume;
             [NonSerialized] public float mosquitoHumCurrentVolume;

            public AudioClip mosquitoDeathAC;
            [Range(0, 1)] public float mosquitoDeathVolume;
            [NonSerialized] public float mosquitoDeathCurrentVolume;

    [Header("Crocodile AudioClips")]

            public AudioClip crocodileAttackAC;
             [Range(0, 1)] public float crocodileAttackVolume;
             [NonSerialized] public float crocodileAttackCurrentVolume;

            public AudioClip crocodileDetectionAC;
             [Range(0, 1)] public float crocodileDetectionVolume;
             [NonSerialized] public float crocodileDetectionCurrentVolume;

            public AudioClip crocodileDeathAC;
             [Range(0, 1)] public float crocodileDeathVolume;
             [NonSerialized] public float crocodileDeathCurrentVolume;

    [Header("Level 2 Props AudioClips")]

        public AudioClip campfireAC;
         [Range(0, 1)] public float campfireVolume;
         [NonSerialized] public float campfireCurrentVolume;

        public AudioClip factoryAC;
         [Range(0, 1)] public float factoryVolume;
         [NonSerialized] public float factoryCurrentVolume;

        public AudioClip lightedSignAC;
         [Range(0, 1)] public float lightedSignVolume;
         [NonSerialized] public float lightedSignCurrentVolume;

        public AudioClip snowplowAC;
         [Range(0, 1)] public float snowplowVolume;
         [NonSerialized] public float snowplowCurrentVolume;
 
    [Header("Test Dialogues AudioClips")]
     public AudioClip testDialogueAC;
      [Range(0, 1)] public float testDialogueVolume;
      [NonSerialized] public float testDialogueCurrentVolume;

    [Header("Test Music AudioClips")]
     public AudioClip testMusicAC;
      [Range(0, 1)] public float testMusicVolume;
      [NonSerialized] public float testMusicCurrentVolume;
}
