using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;


/*************************************************************************************
 * 
 *			Class       : MusicPlayer
 *			Date        : 06/05/2015 12:25:15
 *			Author      : Matthieu
 *			Comment		: Manage MusicPlayer
 * 
*************************************************************************************/

namespace NeoarcadiaescapeRoom
{
    /// ---------------------------------------------------------------------
    /// <summary>
    /// This class defined a MusicPlayer
    /// </summary>
    /// ---------------------------------------------------------------------

    class MusicPlayer
    {
       
        private bool _isPlaying=false;
        private string _currentFile = "";
        private string lastFile = "";
        private string shortName = "";
        public bool IsPlaying
        {
            get { return _isPlaying; }
            set { _isPlaying = value; }
        }
         [DllImport("winmm.dll")]
        private static extern long mciSendString(string lpstrCommand, StringBuilder lpstrReturnString, int uReturnLength, int hwndCallback);
       

        private string getType(string file)
         {
             return "MPEGVideo";
             //if (file.Contains(".mp3")) return "MPEGVideo";
             //return "waveaudio";
         }

        public void Open(string file)
        {

                shortName = file.Split('\\').Last().Replace("-","").Replace("(","").Replace(")","").Replace("_","").Replace("@","").Replace(" ","").Replace("'","");
                string command = "open \"" + file + "\" type " + getType(file) + " alias " + shortName;
                
                mciSendString(command, null, 0, 0);
            
            _currentFile = file;
        }

        public void Play(bool loop = false)
        {
            if (_isPlaying)
                Stop();


            string command = "play " + shortName+" from 0";
                if (loop)
                    command += " REPEAT";
               
                mciSendString(command, null, 0, 0);
           
            lastFile = _currentFile;
            _isPlaying = true;
        }

        public void Stop()
        {


            string command = "stop " + shortName;
                mciSendString(command, null, 0, 0);

                command = "close " + shortName;
                mciSendString(command, null, 0, 0);
          
            _isPlaying = false;
        }
    }
}
