using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class MenuSwiper : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] float percentThreshold;
    [SerializeField] float tweenTime;
    [SerializeField] GameObject[] pages;
    [SerializeField] MenuBottomBar menuBottomBar;

    [SerializeField] Color[] menuColors;
    [SerializeField] Image backgroundTint;

    Vector3 _panelLocation;

    int _currentPage = 1;


    private void Awake()
    {
        _panelLocation = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        float delta = eventData.pressPosition.x - eventData.position.x;

        if (delta > 0 && _currentPage == pages.Length-1) return;
        if (delta < 0 && _currentPage == 0) return;
        transform.position = _panelLocation - new Vector3(delta, 0, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float percentage = (eventData.pressPosition.x - eventData.position.x) / Screen.width;

        if(Mathf.Abs(percentage) >= percentThreshold)
        {
            Vector3 newLocation = _panelLocation;

            if(percentage > 0 && _currentPage < pages.Length-1)
            {
                newLocation+= new Vector3(-Screen.width, 0, 0);
                _currentPage++;
            }
            else if (percentage < 0 && _currentPage > 0)
            {
                newLocation += new Vector3(Screen.width, 0, 0);
                _currentPage--;
            }

            transform.DOMoveX(newLocation.x, tweenTime);
            backgroundTint.DOColor(menuColors[_currentPage], tweenTime);
            _panelLocation = newLocation;

            menuBottomBar.SetCurrentMenu(_currentPage);

        }
        else
        {
            transform.position = _panelLocation;
        }   
    }

    public void SetNewPage(int i)
    {
        float delta = _currentPage - i;
        _panelLocation += new Vector3(Screen.width*delta, 0, 0);
        _currentPage = i;

        transform.DOMoveX(_panelLocation.x, tweenTime);
        backgroundTint.DOColor(menuColors[_currentPage], tweenTime);
    }
}
