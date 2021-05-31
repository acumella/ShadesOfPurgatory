using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static AudioClip playerSlash, playerDamaged, enemyAttack, bulletRelease, bulletImpact, soulPickup, footstep;

    public static float volume = 1f;
    public static float pitch = 1f;

    private static AudioSource source;
    void Start()
    {
        playerSlash = Resources.Load<AudioClip>("playerSlash"); 
        playerDamaged = Resources.Load<AudioClip>("playerDamaged");
        enemyAttack = Resources.Load<AudioClip>("enemyAttack");
        bulletRelease = Resources.Load<AudioClip>("bulletRelease");
        bulletImpact = Resources.Load<AudioClip>("bulletImpact");
        soulPickup = Resources.Load<AudioClip>("soulPickup");
        footstep = Resources.Load<AudioClip>("footstep");

        source = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "playerSlash":
                source.PlayOneShot(playerSlash);
                break;
            case "playerDamaged":
                source.PlayOneShot(playerDamaged);
                break;
            case "enemyAttack":
                source.PlayOneShot(enemyAttack);
                break;
            case "bulletRelease":
                source.PlayOneShot(bulletRelease);
                break;
            case "bulletImpact":
                source.PlayOneShot(bulletImpact);
                break;
            case "soulPickup":
                source.PlayOneShot(soulPickup);
                break;
            case "footstep":
                source.PlayOneShot(footstep);
                break;
        }
    }
}
