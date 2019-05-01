using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutScene : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector timeline;

    [SerializeField]
    private GameObject cutsceneCam;

    public bool isCutscene = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(timeline.state != PlayState.Playing)
        {
            cutsceneCam.SetActive(false);
            isCutscene = false;
        }
	}
}
