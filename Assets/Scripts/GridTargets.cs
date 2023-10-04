using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TMPro.Fitts
{
    public class GridTargets : MonoBehaviour
    {
        // grid
        public GameObject _targetPrefab;
        GameObject[,] _grid;

        // audio clip 
        public AudioClip _aClip;

        // var: elapsed time
        float _elapsedTime;

        int _statClicked = 0;
        int _statTotalClicks = 0;
        float _statTotalTime = 0.0f;
        float _statAccuracy = 0.0f;

        // related UI
        //public Text _statUI;
        public TextMeshProUGUI _statUI;
        public GameObject _startUI;

        // Start is called before the first frame update
        void Start()
        {
            // zero elapsed time
            _elapsedTime = 0.0f;

            //find (or isntantiate) all spheres in scene
            for (int i = 0; i < 5; i++)
            {
                float x = (1.25f * i) - 2.5f;

                for (int j = 0; j < 5; j++)
                {
                    float y = (1.25f * j) - 2.5f;

                    Instantiate(_targetPrefab, new Vector3(x, y, 10f), Quaternion.identity);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            // update elapsed time
            _elapsedTime += Time.deltaTime;
            _statTotalTime += Time.deltaTime;
            _statAccuracy = (100.0f * _statClicked) / _statTotalClicks;
            _statUI.text = "Time Elapsed: " + (_statTotalTime).ToString() + "\n" +
                    "Accuracy: " + (_statAccuracy).ToString() + "\n" +
                    "Spheres Clicked: " + (_statClicked).ToString();

            if (_statTotalTime > 10.0f)
            {
                Debug.Log("Test completed.");

                _statUI.text = "Accuracy: " + (_statAccuracy).ToString() + "\n" +
                    "Spheres Clicked: " + (_statClicked).ToString() + "\n" +
                    "Total Spheres: " + (_statTotalClicks).ToString();
                //                    "avg distance: " + (_statTotalDistance / trials).ToString() + "\n" +
                //                   "avg time: " + (_statTotalTime / trials).ToString();

                //gameObject.SetActive(false);
                _startUI.SetActive(true);

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
            //            _elapsedTime = 0.0f;
            _statClicked = 0;
            //            _statTotalDistance = 0f;
            _statTotalTime = 0f;
            _statTotalClicks = 0;

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
            _statClicked++;

            float x;
            float y;
            if (transform.localPosition == new Vector3(0f, 2.5f, 10f))
            {
                // move the target to new location
                // i.e. range between (-8, 0, 10) to (8, 4, 10)
                x = Random.Range(-10f, 10f);
                y = Random.Range(0f, 5f);

                _elapsedTime = 0f;
            }
            else
            {
                x = 0f;
                y = 2.5f;
            }
            transform.localPosition = new Vector3(x, y, 10f);

            // increment trial count
            _statTotalClicks++;
        }
    }
}
