using System.Collections;
using UnityEngine;

//Rurns off explosion prefab after some time
public class ExplosionController : MonoBehaviour
{
    public float duration;

    private void OnEnable()
    {
        StartCoroutine(DisableParticleAfterTime(duration));
    }

    IEnumerator DisableParticleAfterTime(float dur)
    {
        yield return new WaitForSeconds(dur);
        gameObject.SetActive(false);
    }
}
