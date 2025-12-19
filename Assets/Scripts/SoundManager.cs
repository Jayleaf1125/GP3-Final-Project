using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    AudioSource backgroundMusicSound;
    AudioSource playerDamageSound;
    AudioSource enemyDamageSound;
    AudioSource playerJumpSound;
    AudioSource enemyAttackSound;
    AudioSource enemyDeathSound;
    AudioSource playerDeathSound;
    AudioSource checkpointSound;
    AudioSource playerSacfriceSound;
    AudioSource playerAttackSound;

    private protected override void Awake()
    {
        base.Awake();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioSource[] allAudioSources = GetComponents<AudioSource>();
        backgroundMusicSound = allAudioSources[0];
        playerDamageSound = allAudioSources[1];
        enemyDamageSound = allAudioSources[2];
        playerJumpSound = allAudioSources[3];
        enemyAttackSound = allAudioSources[4];
        enemyDeathSound = allAudioSources[5];
        playerDeathSound = allAudioSources[6];
        checkpointSound = allAudioSources[7];
        playerSacfriceSound = allAudioSources[8];
        playerAttackSound = allAudioSources[9];
    }

    public void PlayBackgroundMusicSound() => backgroundMusicSound.Play();
    public void PlayPlayerDamageSound() => playerDamageSound.Play();
    public void PlayEnemyDamageSound() => enemyDamageSound.Play();
    public void PlayPlayerJumpSound() => playerJumpSound.Play();
    public void PlayEnemyAttack() => enemyAttackSound.Play();
    public void PlayEnemyDeathSound() => enemyDeathSound.Play();
    public void PlayPlayerDeathSound() => playerDeathSound.Play();
    public void PlayCheckpointSound() => checkpointSound.Play();
    public void PlayPlayerSacfriceSound() => playerSacfriceSound.Play();
    public void PlayPlayerAttackSound() => playerAttackSound.Play();
}
