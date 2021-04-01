using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectHit : MonoBehaviour
{
    [SerializeField] float reloadLevelDelay = 2f;
    [SerializeField] AudioClip successAudio;
    [SerializeField] AudioClip failureAudio;

    AudioSource m_MyAudioSource;
    bool isTransitioning = false;

    private void Start()
    {
        m_MyAudioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        switch (other.collider.tag)
        {
            case "Friendly":
                Debug.Log("You hit a friendly");
                
                break;
            case "Finish":
                isTransitioning = true;
                StartSuccessSequence();
                isTransitioning = false;
                break;
            default:
                isTransitioning = true;
                StartCrachSequence();
                isTransitioning = false;
                break;
        }
    }

    void StartSuccessSequence()
    {
        m_MyAudioSource.Stop();
        IsTransitioningCleanup(successAudio);
        Invoke(nameof(NextLevel), reloadLevelDelay);
    }

    void StartCrachSequence()
    {
        //Add sound effect upon crash
        m_MyAudioSource.Stop();
        IsTransitioningCleanup(failureAudio);
        Invoke(nameof(ReloadLevel), reloadLevelDelay);

    }

    void ReloadLevel()
    {
       
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        

    }


    void NextLevel()
    {
      
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
        
    }

    // This function disables movement and sound so while preparing for the next level
    void IsTransitioningCleanup(AudioClip audio)
    {
        GetComponent<Movement>().enabled = !isTransitioning;
        m_MyAudioSource.PlayOneShot(audio);
        //m_MyAudioSource.enabled = !isTransitioning;


    }
}

