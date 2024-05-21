using System;
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

        public AudioClip bufoJump1AC;
         [Range(0, 1)] public float bufoJump1Volume;
         [NonSerialized] public float bufoJump1CurrentVolume;

        public AudioClip bufoJump2AC;
         [Range(0, 1)] public float bufoJump2Volume;
         [NonSerialized] public float bufoJump2CurrentVolume;

        public AudioClip bufoJump3AC;
         [Range(0, 1)] public float bufoJump3Volume;
         [NonSerialized] public float bufoJump3CurrentVolume;

        public AudioClip bufoTongueImpAC;
         [Range(0, 1)] public float bufoTongueImpVolume;
         [NonSerialized] public float bufoTongueImpCurrentVolume;

        public AudioClip bufoFlutterAC;
         [Range(0, 1)] public float bufoFlutterVolume;
         [NonSerialized] public float bufoFlutterCurrentVolume;

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

        public AudioClip bufoDeath1AC;
         [Range(0, 1)] public float bufoDeath1Volume;
         [NonSerialized] public float bufoDeath1CurrentVolume;

        public AudioClip bufoDeath2AC;
         [Range(0, 1)] public float bufoDeath2Volume;
         [NonSerialized] public float bufoDeath2CurrentVolume;

        public AudioClip bufoBiteAC;
         [Range(0, 1)] public float bufoBiteVolume;
         [NonSerialized] public float bufoBiteCurrentVolume;

        public AudioClip bufoHurt1AC;
         [Range(0, 1)] public float bufoHurt1Volume;
         [NonSerialized] public float bufoHurt1CurrentVolume;

        public AudioClip bufoHurt2AC;
         [Range(0, 1)] public float bufoHurt2Volume;
         [NonSerialized] public float bufoHurt2CurrentVolume;

        public AudioClip bufoHurt3AC;
         [Range(0, 1)] public float bufoHurt3Volume;
         [NonSerialized] public float bufoHurt3CurrentVolume;

        public AudioClip bufoHurt4AC;
         [Range(0, 1)] public float bufoHurt4Volume;
         [NonSerialized] public float bufoHurt4CurrentVolume;

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

        public AudioClip bananaAC;
         [Range(0, 1)] public float bananaVolume;
         [NonSerialized] public float bananaCurrentVolume;

        public AudioClip buttonAC;
         [Range(0, 1)] public float buttonVolume;
         [NonSerialized] public float buttonCurrentVolume;

        public AudioClip timeTrialNormalAC;
         [Range(0, 1)] public float timeTrialNormalVolume;
         [NonSerialized] public float timeTrialNormalCurrentVolume;

        public AudioClip timeTrialUrgentAC;
         [Range(0, 1)] public float timeTrialUrgentVolume;
         [NonSerialized] public float timeTrialUrgentCurrentVolume;

        public AudioClip timeTrialEndAC;
         [Range(0, 1)] public float timeTrialEndVolume;
         [NonSerialized] public float timeTrialEndCurrentVolume;

        public AudioClip fenceAC;
         [Range(0, 1)] public float fenceVolume;
         [NonSerialized] public float fenceCurrentVolume;

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
