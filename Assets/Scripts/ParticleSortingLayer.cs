using UnityEngine;
using System.Collections;

public class ParticleSortingLayer : MonoBehaviour {
    public string Layer;

    // Use this for initialization
    void Start()
    {
        //Change Foreground to the layer you want it to display on 
        //You could prob. make a public variable for this
        GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = Layer;
    }

    // Update is called once per frame
    void Update () {
	
	}
}
