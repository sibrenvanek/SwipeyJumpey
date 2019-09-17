using UnityEngine;
using System.Collections;
using DG.Tweening;
using DG.Tweening.Core;

public class Jet : MonoBehaviour
{
    [SerializeField] private new ParticleSystem particleSystem = null;
    public DOSetter<float> LifeTimeSetter { get; private set; }
    public DOGetter<float> LifeTimeGetter { get; private set; }
    public DOSetter<float> SizeSetter { get; private set; }
    public DOGetter<float> SizeGetter { get; private set; }

    private void Awake() 
    {
        SetUpDOGettersAndSetters();
    }

    private void OnValidate() {
        particleSystem = GetComponent<ParticleSystem>();
    }

    public void StartEngine()
    {
        particleSystem.Play();
    }

    public void StopEngine()
    {
        DOTween.To(LifeTimeGetter, LifeTimeSetter, 0, .2f);
        DOTween.To(SizeGetter, SizeSetter, 0, .2f);
        StartCoroutine(WaitAndStopParticleSystem(.2f));
    }

    private IEnumerator WaitAndStopParticleSystem(float t)
    {
        yield return new WaitForSeconds(t);
        particleSystem.Stop();
    }

    public void Accelerate(float startLifeTime, float startSize)
    {
        DOTween.To(LifeTimeGetter, LifeTimeSetter, startLifeTime, .2f);
        DOTween.To(SizeGetter, SizeSetter, startSize, .2f);
    }

    private void SetUpDOGettersAndSetters()
    {
        LifeTimeGetter = new DOGetter<float>(() => {
            return particleSystem.main.startLifetime.constant;
        });

        LifeTimeSetter = new DOSetter<float>((lifetime) => {
            var main = particleSystem.main;
            main.startLifetime = lifetime;
        });

        SizeGetter = new DOGetter<float>(() => {
            return particleSystem.main.startSize.constant;
        });

        SizeSetter = new DOSetter<float>((size) => {
            var main = particleSystem.main;
            main.startSize = size;
        });
    }
    
}