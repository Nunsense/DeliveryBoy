using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use
		private int screenWidth;

        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
			screenWidth = Screen.width;
        }


        private void FixedUpdate()
        {

#if UNITY_EDITOR || UNITY_WEBPLAYER
            // pass the input to the car!
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            float handbrake = Input.GetAxis("Jump");
            m_Car.Move(h, v, v, handbrake);
#else
			if(Input.touchCount > 1){
				m_Car.Move(0, -1, -1, 0);
			}else{
				Touch touch = Input.GetTouch(0);
				m_Car.Move(((touch.position.x * 2 / screenWidth) - 1), 1, 1, 0);
			}
#endif
        }
    }
}
