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
        int _activeTargets;

        // audio clip 
        public AudioClip _aClip;

        // var: elapsed time
        bool _testBegun;
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
            _testBegun = false;

            _grid = new GameObject[5, 5];
            _activeTargets = 0;

            //find (or isntantiate) all spheres in scene
            for (int i = 0; i < 5; i++)
            {
                float x = (1.25f * i) - 2.5f;

                for (int j = 0; j < 5; j++)
                {
                    float y = 6.25f - (1.25f * j);

                    _grid[i, j] = Instantiate(_targetPrefab, new Vector3(x, y, 10f), Quaternion.identity);
                    _grid[i, j].SetActive(false);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            // update elapsed time
            if (_testBegun)
            {
                _elapsedTime += Time.deltaTime;
                _statTotalTime += Time.deltaTime;
                _statAccuracy = (100.0f * _statClicked) / _statTotalClicks;
                _statUI.text = "Time Elapsed: " + (_statTotalTime).ToString() + "\n" +
                        "Accuracy: " + (_statAccuracy).ToString() + "\n" +
                        "Spheres Clicked: " + (_statClicked).ToString();
            }

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
                _testBegun = false;
                return;
            }
        }

        public void StartTest()
        {
            _testBegun = true;
            // randomly activate 3 spheres
            while (_activeTargets < 3)
            {
                int x = Random.Range(0, 5);
                int y = Random.Range(0, 5);
                if (_grid[x, y].activeSelf == false)
                {
                    _grid[x, y].SetActive(true);
                    _activeTargets++;
                }
            }

            // initialize the prev position with the current
            //            _prevPosition = transform.localPosition;

            // zero elapsed time
            _elapsedTime = 0.0f;
            _statClicked = 0;
            //            _statTotalDistance = 0f;
            _statTotalTime = 0f;
            _statTotalClicks = 0;



            Debug.Log("Test started.");
        }

        // call back for select event from the controller
        public void TargetClicked()
        {
            Debug.Log("Target selected at " + transform.position);

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

           

            // increment trial count
            _statTotalClicks++;
        }
    }
}
