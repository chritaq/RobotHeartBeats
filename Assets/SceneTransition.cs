using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    [SerializeField]
    private RectTransform blackScreenTransform;
    private float startPositionX;
    private float endPositionX;
    private float activePositionX;

    

    public bool isPlaying = false;

    [SerializeField]
    private bool endTransition = false;

    // Start is called before the first frame update
    void Start()
    {
        startPositionX = blackScreenTransform.anchoredPosition.x;
        endPositionX = -blackScreenTransform.anchoredPosition.x;
        activePositionX = 0;
        if(endTransition)
        {
            EndTransition();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTransition()
    {
        isPlaying = true;
        blackScreenTransform.anchoredPosition = new Vector2(startPositionX, 0);
        activePositionX = startPositionX;
        StartCoroutine("SlideIn");
    }

    public void EndTransition()
    {
        isPlaying = true;
        blackScreenTransform.anchoredPosition = new Vector2(0, 0);
        activePositionX = 0;
        StartCoroutine("SlideOut");
    }


    private float slideRate = 0.1f;
    private IEnumerator SlideIn()
    {
        while(activePositionX > 0)
        {
            activePositionX-= 20f;
            blackScreenTransform.anchoredPosition = new Vector2(activePositionX, 0);
            yield return new WaitForEndOfFrame();
        }
        isPlaying = false;
        yield return null;
    }

    private IEnumerator SlideOut()
    {
        while (activePositionX > endPositionX)
        {
            activePositionX -= 20f;
            blackScreenTransform.anchoredPosition = new Vector2(activePositionX, 0);
            yield return new WaitForEndOfFrame();
        }
        isPlaying = false;
        yield return null;
    }
}
