using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HeartsDisplay : MonoBehaviour
{
    [SerializeField] GameObject heartDisplayPrefab;

    [SerializeField] Transform heartsLayoutTransform;

    [SerializeField] Sprite heartSprite;

    List<GameObject> _hearts = new List<GameObject>();

    AudioSource _audioSource;

    int _totalHearts;
    int _currentIdx;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        BoardEventManager.OnLevelLoaded += InitHearts;
        BoardEventManager.OnObjectiveAchieved += OnObjectiveAchieved;
    }

    private void OnDisable()
    {
        BoardEventManager.OnLevelLoaded -= InitHearts;
        BoardEventManager.OnObjectiveAchieved -= OnObjectiveAchieved;
    }
    void InitHearts(Level l)
    {
        _totalHearts = l.Objectives.Length;

        for (int i = 0; i < _totalHearts; i++)
        {
            var heart = Instantiate(heartDisplayPrefab, heartsLayoutTransform);

            _hearts.Add(heart);
        }
    }

    void OnObjectiveAchieved()
    {
        var heart = _hearts[_currentIdx++];
        heart.GetComponent<RectTransform>().DOShakeScale(0.3f);
        heart.GetComponent<Image>().sprite = heartSprite;
        _audioSource.Play();
    }

}
