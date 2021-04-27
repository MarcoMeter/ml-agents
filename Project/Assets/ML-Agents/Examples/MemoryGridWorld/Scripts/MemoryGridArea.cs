using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGridArea : MonoBehaviour
{
    [SerializeField]
    public GameObject _hintCircle;
    [SerializeField]
    public GameObject _hintKey;
    [SerializeField]
    public GameObject _topCircle;
    [SerializeField]
    public GameObject _topKey;
    [SerializeField]
    public GameObject _bottomCircle;
    [SerializeField]
    public GameObject _bottomKey;

    public void Reset()
    {
        // Disable all entities
        _hintCircle.SetActive(false);
        _hintKey.SetActive(false);
        _topCircle.SetActive(false);
        _topCircle.tag = "Finish";
        _topKey.SetActive(false);
        _topKey.tag = "Finish";
        _bottomCircle.SetActive(false);
        _bottomCircle.tag = "Finish";
        _bottomKey.SetActive(false);
        _bottomKey.tag = "Finish";

        // Randomize hint
        int randomHint = Random.Range(0, 2);
        if(randomHint == 0)
        {
            _hintCircle.SetActive(true);
        }
        else
        {
            _hintKey.SetActive(true);
        }

        // Randomize exits
        int randomExit = Random.Range(0, 2);
        if(randomExit == 0)
        {
            _topCircle.SetActive(true);
            _bottomKey.SetActive(true);
            if (randomHint == 0)
            {
                _topCircle.tag = "goal";
            }
            else
            {
                _bottomKey.tag = "goal";
            }
        }
        else
        {
            _topKey.SetActive(true);
            _bottomCircle.SetActive(true);
            if (randomHint == 0)
            {
                _bottomCircle.tag = "goal";
            }
            else
            {
                _topKey.tag = "goal";
            }
        }
    }
}
