using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace TMPro.Fitts
{
    public class Countdown : MonoBehaviour
    {
        public float _startTime;
        public UnityEvent function;
        public TextMeshProUGUI _countdownUI;
        public GameObject[] _startUI;

        // Start is called before the first frame update
        void Start()
        {
            _startTime = 3f;
        }

        // Update is called once per frame
        void Update()
        {
            _startTime = _startTime - Time.deltaTime;

            _countdownUI.text = _startTime.ToString();

            if (_startTime <= 0f)
            {
                function.Invoke();
                gameObject.SetActive(false);
                return;
            }
        }

        public void CountdownStart()
        {
            gameObject.SetActive(true);
            _startTime = 3f;
            for (int i = 0; i < _startUI.Length; i++)
            {
                _startUI[i].SetActive(false);
            }
        }
    }
}
