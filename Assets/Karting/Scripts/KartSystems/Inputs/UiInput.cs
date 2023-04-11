using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KartGame.KartSystems
{
    public class UiInput : BaseInput

    {
        public bool accelerate;
        public bool brake;
        public float turn;
        public void StartAccelerate() => accelerate = true;

        public void StopAccelerate() => accelerate = false;

        public void StartBrake() => brake = true;
        public void StopBrake() => brake = false;

        public void StartTurnLeft() => turn = Mathf.Max(turn - 0.8f, -1);
        public void StopTurnLeft() => turn = Mathf.Min(turn + 0.8f, 1);

        public void StartTurnRight() => turn = Mathf.Min(turn + 0.8f, 1);
        public void StopTurnRight() => turn = Mathf.Max(turn - 0.8f, -1);

        public override InputData GenerateInput()
        {
            return new InputData
            {
                Accelerate = accelerate,
                Brake = brake,
                TurnInput = turn
            };
        }
    }
}
