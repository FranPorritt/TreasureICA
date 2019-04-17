using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour {

    public Animator animator;
    public Animator bridgeAnimator;

    public bool isLeverPulled = false;

    [SerializeField]
    private GameObject LeverTextPrefab;
    [SerializeField]
    private PlayerController player;

    // Use this for initialization
    void Start ()
    {
        LeverTextPrefab.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!isLeverPulled)
        {
            if (player.atLever)
            {
                LeverTextPrefab.SetActive(true);
            }
            else
            {
                LeverTextPrefab.SetActive(false);
            }
        }

		if (isLeverPulled)
        {
            LeverTextPrefab.SetActive(false);
            animator.SetTrigger("LeverPull");
            bridgeAnimator.SetTrigger("LowerBridge");
        }
	}
}
