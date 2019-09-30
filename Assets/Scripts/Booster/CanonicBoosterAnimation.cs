using UnityEngine;

public class CanonicBoosterAnimation : MonoBehaviour {
    private CanonicBooster canonicBooster = null;
    private Animator animator = null;
    private void Awake() {
        canonicBooster = GetComponent<CanonicBooster>();    
        animator = GetComponent<Animator>();
    }

    private void Update() 
    {
        animator.SetBool("Charging", canonicBooster.HoldingPlayer);
    }
}