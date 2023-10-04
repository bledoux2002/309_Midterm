using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace TMPro.Fitts
{
    public class Strafe : MonoBehaviour
    {
        // audio clip 
        public AudioClip _aClip;

        // var: elapsed time
        float _elapsedTime;

        // health
        float _health;

        bool _beingTracked;
        int changeDir;
        float _statTrackedTime = 0;
        int _statTotalCount = 0;
        float _statTotalTime = 0.0f;
        //        float _statTotalDistance = 0.0f;
        float _statAccuracy = 0.0f;

        // related UI
        //public Text _statUI;
        public TextMeshProUGUI _statUI;
        public GameObject _startUI;

        // Start is called before the first frame update
        void Start()
        {
            // zero elapsed time
            _statTotalTime = 0.0f;
            _elapsedTime = 0.0f;
            _beingTracked = false;
            _health = 100.0f;
            changeDir = 1;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            //randomize direction
            int randInt = Random.Range(0, 100);
            if ((randInt == 50) || (Math.Abs(transform.position.x) > 10.0f))
            {
                changeDir = changeDir * -1;
            }

            //move target
            transform.position = new Vector3(transform.position.x + (0.1f * changeDir), 2.5f, 10f);

            // update elapsed time
            _elapsedTime += Time.deltaTime;
            _statTotalTime += Time.deltaTime;
            if (_beingTracked)
            {
                _statTrackedTime += Time.deltaTime;
                _health = _health - 0.001f;

                if (_health <= 0f)
                {
                    Debug.Log("Target Destroyed");
                    _health = 100.0f;
                    transform.position = new Vector3(Random.Range(-10.0f, 10.0f), 2.5f, 10.0f);
                    // increment trial count
                    _statTotalCount++;
                }

                _beingTracked = false;
            }
            _statAccuracy = (100.0f * _statTrackedTime) / _statTotalTime;
            _statUI.text = "Time Elapsed: " + (_statTotalTime).ToString() + "\n" +
                    "Accuracy: " + (_statAccuracy).ToString() + "\n" +
                    "Targets Tracked: " + (_statTotalCount).ToString() + "\n" +
                    "Current Target Health: " + (_health).ToString();

            if (_statTotalTime > 10.0f)
            {
                Debug.Log("Test completed.");

                _statUI.text = "Time Elapsed: " + (_statTotalTime).ToString() + "\n" +
                    "Accuracy: " + (_statAccuracy).ToString() + "\n" +
                    "Targets Tracked: " + (_statTotalCount).ToString();
                //                    "avg distance: " + (_statTotalDistance / trials).ToString() + "\n" +
                //                   "avg time: " + (_statTotalTime / trials).ToString();

                gameObject.SetActive(false);
                _startUI.SetActive(true);
                transform.localPosition = new Vector3(0f, 2.5f, 10f);

                // need to stop the test
                return;
            }
        }

        public void StartTest()
        {
            gameObject.SetActive(true);

            // initialize the prev position with the current
            //            _prevPosition = transform.localPosition;

            // zero elapsed time
            _elapsedTime = 0.0f;
            _statTrackedTime = 0;
            //            _statTotalDistance = 0f;
            _statTotalTime = 0f;
            _statTotalCount = 0;

            Debug.Log("Test started.");
        }

        // call back for select event from the controller
        public void TargetClicked()
        {
            Debug.Log("Target selected.");

            // test: play some effect sound
            AudioSource.PlayClipAtPoint(_aClip, transform.position);

            // compute distance from the prev to current
            //            float dist = Vector3.Distance(_prevPosition, transform.localPosition);
            //            _statTotalDistance += dist;

            // update prev pos
            //            _prevPosition = transform.localPosition;

            // calc time taken and reset elapsed time
            //            float tasktime = _elapsedTime;
            //            _statTotalTime += _elapsedTime;
            //            _elapsedTime = 0.0f;

            // store distance and task time to the file
            //            string outstr = dist.ToString() + "\t" + tasktime.ToString();
            //            DataLogger.Log(outstr);
            //            _statUI.text = _count.ToString() + " : " + outstr;

            //update accuracy
            _beingTracked = true;
        }
    }
}
