
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace LCSeedPicker.Components
{
    public class SeedInput : MonoBehaviour
    {

        public bool isVisible;
        private string _seedInputString = "";
        private string _seedOutputString = "";

        private int _maxSeedLength = 100;
        private float _inputWidth = 0f;
        private float _inputHeight = 0f;

        private GUIStyle _inputStyle;

        public void Awake()
        {
            DontDestroyOnLoad(this);
            isVisible = false;

            Plugin.Logger.LogInfo("SeedInput created and Awake.");
            // Set input size based on screen size
            // TODO: add minimum size.
            _inputWidth = Math.Max(Screen.width / 12f, 100f);
            _inputHeight = Math.Max(Screen.height / 12f, 10f);

        }
        
        void OnGUI()
        {
            if (!isVisible) return;

            if (_inputStyle == null)
            {
                _inputStyle = new GUIStyle(GUI.skin.textField);
            }

            float inputX = _inputWidth*2;
            float inputY = Screen.height / 2 - _inputHeight / 2;

            GUILayout.BeginArea(new Rect(inputX, inputY, _inputWidth, _inputHeight));
            GUILayout.Label("Seed (empty for random):");
            _seedInputString = FilterSeedInput(GUILayout.TextField(_seedInputString, _maxSeedLength, _inputStyle));
            GUILayout.EndArea();

            GUILayout.BeginArea(new Rect(inputX+_inputWidth, inputY, _inputWidth, _inputHeight));
            GUILayout.Label("Last random seed:");
            GUILayout.TextArea(_seedOutputString, _inputStyle);
            GUILayout.EndArea();
        }

        private string FilterSeedInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return "";

            return new string(input.Where(c => char.IsDigit(c) || c == '.').ToArray());
        }
        public int GetSeed()
        {
            int seed;
            try
            {
                // Convert seed to int
                seed = int.Parse(_seedInputString);

            } catch (Exception)
            {
                seed = -1;
            }

            if (seed < 0 || seed > Math.Pow(10, _maxSeedLength)) seed = -1;
            
            return seed;
        }

        public void SetLastSeed(int lastSeed)
        {
            _seedOutputString = $"{lastSeed}";
        }

        public void DestroyInput()
        {
            Destroy(this);
        }
    }
}