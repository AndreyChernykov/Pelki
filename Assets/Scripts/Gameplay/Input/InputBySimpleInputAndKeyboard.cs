using Pelki.Configs;
using UnityEngine;

namespace Pelki.Gameplay.Input
{
    public class InputBySimpleInputAndKeyboard : IInput
    {
        public float Horizontal => GetHorizontal();
        public bool IsJump => GetHasJump();

        private readonly InputBySimpleInput simpleInput;
        private readonly InputConfig inputConfig;

        public InputBySimpleInputAndKeyboard(InputConfig inputConfig)
        {
            this.inputConfig = inputConfig;
            simpleInput = new InputBySimpleInput(inputConfig);
        }

        private float GetHorizontal()
        {
            float result = 0;
            float pcHorizontal = UnityEngine.Input.GetAxis(inputConfig.HorizontalAxisKey);
            result = pcHorizontal;

            if (pcHorizontal == 0)
            {
                result = simpleInput.Horizontal;
            }

            return result;
        }

        private bool GetHasJump()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                return true;
            }

            return false;
        }
    }
}