// Copyright 2022 James Lee, Connecticut College.
//    All rights reserved.
//
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are
// met:
//
//     * Redistributions of source code must retain the above copyright
//       notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above
//       copyright notice, this list of conditions and the following
//       disclaimer in the documentation and/or other materials provided
//       with the distribution.
//     * Neither the name of Connecticut College nor the names of its
//       contributors may be used to endorse or promote products derived
//       from this software without specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
// "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
// LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
// A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
// OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
// SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
// LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
// DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
// THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
// OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
// Contact: James Lee - james.lee@conncoll.edu

/* DataLogger.cs
 * brief description
 * 
 * 
 * 
*/

using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;

public class DataLogger {

	public static string DataLogFile = "";
    static bool _initialized = false;

	public DataLogger()
    {

		if (DataLogFile.Length == 0)
            // just one log file???
            DataLogFile = Application.persistentDataPath + "/DataLog.txt";
	}

    static void Initialize()
    {
        _initialized = true;

    }

    public static void Log(string log)
    {
        // initialization
        if (_initialized != true)
            Initialize();

        // format -> date#log
        string output = "";
		output += DateTime.Now.ToString ();
        output += ("\t" + log + "\n");

        if (DataLogFile.Length == 0)
            DataLogFile = Application.persistentDataPath + "/DataLog.txt";

        // add to a local log file
        File.AppendAllText(DataLogFile, output);

    }

    // testing purpose
    public static void ReadAll() {

		// read all lines & print all
		if (DataLogFile.Length == 0)
            DataLogFile = Application.persistentDataPath + "/DataLog.txt";

		if (!File.Exists (DataLogFile)) {
		
			Debug.Log("log file not found");
		
		} else {

			string fileText = File.ReadAllText (DataLogFile);
            Debug.Log("log entry: " + fileText);
		}
	}

}