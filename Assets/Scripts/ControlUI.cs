using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ControlUI : MonoBehaviour {

    public TouchDetector touchDetector;

    [SerializeField] List<Image> listOfImgSlider;
    [SerializeField] Image imgSliderCircle;
    [SerializeField] List<Sprite> listOfSliderCircleSprite;
    private bool isSwiping;
    private int sliderImageId = 0;

    // Use this for initialization
    void Start () {
        sliderImageId = 0;
	}
	
	// Update is called once per frame
	void Update () {
        checkSwipe();
    }

    private void checkSwipe()
    {
        if (!touchDetector.IsDraging)
            isSwiping = false;

        if (touchDetector.SwipeRight && !isSwiping)
        {
            if (sliderImageId > 0)
            {
                sliderImageId--;
                slideImage(false);
            }
            isSwiping = true;
        }
        if (touchDetector.SwipeLeft && !isSwiping)
        {
            if (sliderImageId < listOfImgSlider.Count - 1)
            {
                sliderImageId++;
                slideImage(true);
            }
            isSwiping = true;
        }
    }

    private void slideImage(bool _next)
    {
        imgSliderCircle.sprite = listOfSliderCircleSprite[sliderImageId];
        if (_next)
        {
            for (int i = 0; i < listOfImgSlider.Count; i++)
            {
                listOfImgSlider[i].transform.DOLocalMoveX(listOfImgSlider[i].rectTransform.anchoredPosition.x - 352, 0.3f, false);
            }
        }
        else
        {
            for (int i = 0; i < listOfImgSlider.Count; i++)
            {
                listOfImgSlider[i].transform.DOLocalMoveX(listOfImgSlider[i].rectTransform.anchoredPosition.x + 352, 0.3f, false);
            }
        }
    }
}
