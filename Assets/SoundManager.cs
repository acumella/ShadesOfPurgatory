using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static AudioClip playerSlash, playerDamaged, enemyAttack, bulletRelease, bulletImpact;

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
        }
    }

    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
