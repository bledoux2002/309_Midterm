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
        int _activeX;
        int _activeY;

        // audio clip 
        public AudioClip _aClip;

        // var: elapsed time
        bool _testBegun;
        float _elapsedTime;

        int _statClicked;
        int _statTotalCount;
        int _statTotalClicks;
        float _statTotalTime;
        float _statAccuracy;

        // related UI
        //public Text _statUI;
        public TextMeshProUGUI _statUI;
        public GameObject[] _startUI;

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

                /* only for multiple active targets
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        _grid[i, j].SetActive(false);
                    }
                }*/

                _grid[_activeX, _activeY].SetActive(false);

                _activeTargets = 0;

                _statUI.text = "Accuracy: " + (_statAccuracy).ToString() + "\n" +
                    "Spheres Clicked: " + (_statClicked).ToString() + "\n" +
                    "Total Spheres: " + (_statTotalCount).ToString();
                //                    "avg distance: " + (_statTotalDistance / trials).ToString() + "\n" +
                //                   "avg time: " + (_statTotalTime / trials).ToString();

                //gameObject.SetActive(false);
                for (int i = 0; i < _startUI.Length; i++)
                {
                    _startUI[i].SetActive(true);
                }

                if (_testBegun)
                {
                    string outstr = "GRIDSHOT\n" + _statUI.text + "\n";
                    Debug.Log(outstr);
                    DataLogger.Log(outstr);
                    _testBegun = false;
                }

                // need to stop the test
                return;
            }
        }

        public void StartTest()
        {
            for (int i = 0; i < _startUI.Length; i++)
            {
                _startUI[i].SetActive(false);
            }

            _testBegun = true;
            // randomly activate 3 spheres
            
            while (_activeTargets < 1)
            {
                _activeX = Random.Range(0, 5);
                _activeY = Random.Range(0, 5);
                if (_grid[_activeX, _activeY].activeSelf == false)
                {
                    _grid[_activeX, _activeY].SetActive(true);
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
            _statTotalCount = 0;
            _statTotalClicks = 0;

            Debug.Log("Test started.");
        }

        // call back for select event from the controller
        public void TargetClicked()
        {
            Debug.Log("Target selected");

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

            _activeTargets = 0;

            while (_activeTargets < 1)
            {
                int x = Random.Range(0, 5);
                int y = Random.Range(0, 5);
                if (_grid[x, y].activeSelf == false)
                {
                    //deactivaete previous target
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            if (_grid[i, j].activeSelf == true)
                            {
                                _grid[i, j].SetActive(false);
                            }
                        }
                    }

                    _activeX = x;
                    _activeY = y;
                    _grid[_activeX, _activeY].SetActive(true);
                    _activeTargets++;
                }
            }

            // increment trial count
            _statTotalCount++;
        }

        public void PointerActive()
        {
            _statTotalClicks++;
        }
    }
}
