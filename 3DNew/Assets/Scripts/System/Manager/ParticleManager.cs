using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance { get; private set; }

    private Dictionary<string, GameObject> particleDictionary;

    public GameObject[] particlePrefabs;

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }

        particleDictionary = new Dictionary<string, GameObject>();

        foreach (var prefab in particlePrefabs)
        {
            particleDictionary.Add(prefab.name, prefab);
        }

    }

    public void PlayParticle(string particleName, Vector2 position, int layerOrder = 0)
    {

        if (!particleDictionary.ContainsKey(particleName))
        {
            Debug.LogWarning($"해당 파티클 '{particleName}'을(를) 찾을 수 없습니다.");
            return;
        }

        GameObject particle = Instantiate(particleDictionary[particleName], position, Quaternion.identity);

        particle.name = particleName + "_Particle";

        SpriteRenderer spriteRenderer = particle.GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = layerOrder;
        }

        Animator animator = particle.GetComponent<Animator>();

        if (animator != null)
        {
            animator.Play("ParticleAnimation");
        }

        StartCoroutine(DestroyParticleAfterAnimation(particle, animator));

    }

    private IEnumerator DestroyParticleAfterAnimation(GameObject particle, Animator animator)
    {
        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;

        yield return new WaitForSeconds(animationLength);

        Destroy(particle);
    }
}
