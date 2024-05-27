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
         [SerializeField] public float bufoTongueMaxDistance;

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

        public AudioClip checkpointAC;
         [Range(0, 1)] public float checkpointVolume;
         [NonSerialized] public float checkpointCurrentVolume;

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

    [Header("Level 1 Props AudioClips")]

        public AudioClip waterfallAC;
         [Range(0, 1)] public float waterfallVolume;
         [NonSerialized] public float waterfallCurrentVolume;

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

    [Header("Level 3 AudioClips")]

        public AudioClip cannonShootingAC;
         [Range(0, 1)] public float cannonShootingVolume;
         [NonSerialized] public float cannonShootingCurrentVolume; 

        public AudioClip cannonBulletFallingAC;
         [Range(0, 1)] public float cannonBulletFallingVolume;
         [NonSerialized] public float cannonBulletFallingCurrentVolume;

        public AudioClip cannonBulletExplosionAC;
         [Range(0, 1)] public float cannonBulletExplosionVolume;
         [NonSerialized] public float cannonBulletExplosionCurrentVolume;

        public AudioClip cannonBrokenAC;
         [Range(0, 1)] public float cannonBrokenVolume;
         [NonSerialized] public float cannonBrokenCurrentVolume;

    [Header("NPCs AudioClips")]
        //Bufo
        public AudioClip bufoDialogue1AC;
         [Range(0, 1)] public float bufoDialogue1Volume;
         [NonSerialized] public float bufoDialogue1CurrentVolume;

        public AudioClip bufoDialogue2AC;
         [Range(0, 1)] public float bufoDialogue2Volume;
         [NonSerialized] public float bufoDialogue2CurrentVolume;


        public AudioClip bufoDialogue3AC;
         [Range(0, 1)] public float bufoDialogue3Volume;
         [NonSerialized] public float bufoDialogue3CurrentVolume;


        public AudioClip bufoDialogue4AC;
         [Range(0, 1)] public float bufoDialogue4Volume;
         [NonSerialized] public float bufoDialogue4CurrentVolume;

        public AudioClip bufoDialogue5AC;
         [Range(0, 1)] public float bufoDialogue5Volume;
         [NonSerialized] public float bufoDialogue5CurrentVolume;

        //Seda
        public AudioClip sedaDialogue1AC;
         [Range(0, 1)] public float sedaDialogue1Volume;
         [NonSerialized] public float sedaDialogue1CurrentVolume;

        public AudioClip sedaDialogue2AC;
         [Range(0, 1)] public float sedaDialogue2Volume;
         [NonSerialized] public float sedaDialogue2CurrentVolume;

        public AudioClip sedaDialogue3AC;
         [Range(0, 1)] public float sedaDialogue3Volume;
         [NonSerialized] public float sedaDialogue3CurrentVolume;

        public AudioClip sedaDialogue4AC;
         [Range(0, 1)] public float sedaDialogue4Volume;
         [NonSerialized] public float sedaDialogue4CurrentVolume;

        public AudioClip sedaDialogue5AC;
         [Range(0, 1)] public float sedaDialogue5Volume;
         [NonSerialized] public float sedaDialogue5CurrentVolume;

        //Komodo
        public AudioClip komodoDialogue1AC;
         [Range(0, 1)] public float komodoDialogue1Volume;
         [NonSerialized] public float komodoDialogue1CurrentVolume;

        public AudioClip komodoDialogue2AC;
         [Range(0, 1)] public float komodoDialogue2Volume;
         [NonSerialized] public float komodoDialogue2CurrentVolume;

        public AudioClip komodoDialogue3AC;
         [Range(0, 1)] public float komodoDialogue3Volume;
         [NonSerialized] public float komodoDialogue3CurrentVolume;

        public AudioClip komodoDialogue4AC;
         [Range(0, 1)] public float komodoDialogue4Volume;
         [NonSerialized] public float komodoDialogue4CurrentVolume;

        public AudioClip komodoDialogue5AC;
         [Range(0, 1)] public float komodoDialogue5Volume;
         [NonSerialized] public float komodoDialogue5CurrentVolume;

        //Alirón
        public AudioClip alironDialogue1AC;
         [Range(0, 1)] public float alironDialogue1Volume;
         [NonSerialized] public float alironDialogue1CurrentVolume;

        public AudioClip alironDialogue2AC;
         [Range(0, 1)] public float alironDialogue2Volume;
         [NonSerialized] public float alironDialogue2CurrentVolume;

        public AudioClip alironDialogue3AC;
         [Range(0, 1)] public float alironDialogue3Volume;
         [NonSerialized] public float alironDialogue3CurrentVolume;

        public AudioClip alironDialogue4AC;
         [Range(0, 1)] public float alironDialogue4Volume;
         [NonSerialized] public float alironDialogue4CurrentVolume;

        public AudioClip alironDialogue5AC;
         [Range(0, 1)] public float alironDialogue5Volume;
         [NonSerialized] public float alironDialogue5CurrentVolume;

        //Bear
        public AudioClip bearDialogue1AC;
         [Range(0, 1)] public float bearDialogue1Volume;
         [NonSerialized] public float bearDialogue1CurrentVolume;

        public AudioClip bearDialogue2AC;
         [Range(0, 1)] public float bearDialogue2Volume;
         [NonSerialized] public float bearDialogue2CurrentVolume;

        public AudioClip bearDialogue3AC;
         [Range(0, 1)] public float bearDialogue3Volume;
         [NonSerialized] public float bearDialogue3CurrentVolume;

        public AudioClip bearDialogue4AC;
         [Range(0, 1)] public float bearDialogue4Volume;
         [NonSerialized] public float bearDialogue4CurrentVolume;

        //Toucan
        public AudioClip toucanDialogue1AC;
         [Range(0, 1)] public float toucanDialogue1Volume;
         [NonSerialized] public float toucanDialogue1CurrentVolume;

        public AudioClip toucanDialogue2AC;
         [Range(0, 1)] public float toucanDialogue2Volume;
         [NonSerialized] public float toucanDialogue2CurrentVolume;

        public AudioClip toucanDialogue3AC;
         [Range(0, 1)] public float toucanDialogue3Volume;
         [NonSerialized] public float toucanDialogue3CurrentVolume;

        public AudioClip toucanDialogue4AC;
         [Range(0, 1)] public float toucanDialogue4Volume;
         [NonSerialized] public float toucanDialogue4CurrentVolume;

        public AudioClip toucanDialogue5AC;
         [Range(0, 1)] public float toucanDialogue5Volume;
         [NonSerialized] public float toucanDialogue5CurrentVolume;

    [Header("Music AudioClips")]
     public AudioClip mainThemeMusicAC;
      [Range(0, 1)] public float mainThemeMusicVolume;
      [NonSerialized] public float mainThemeMusicCurrentVolume;

     public AudioClip jungleThemeMusicAC;
      [Range(0, 1)] public float jungleThemeMusicVolume;
      [NonSerialized] public float jungleThemeMusicCurrentVolume;

     public AudioClip mountainThemeMusicAC;
      [Range(0, 1)] public float mountainThemeMusicVolume;
      [NonSerialized] public float mountainThemeMusicCurrentVolume;

     public AudioClip swampThemeMusicAC;
      [Range(0, 1)] public float swampThemeMusicVolume;
      [NonSerialized] public float swampThemeMusicCurrentVolume;
}
