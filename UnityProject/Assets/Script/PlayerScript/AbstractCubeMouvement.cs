using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Script
{
    abstract public class AbstractCubeMouvement : MonoBehaviour 
    {
        /*
        * The DeplacementNumber property is use to compute the score
        */

        InGameUIManager MouvementCounterObject;
        private bool IsFalling = false;

        /*
         * The CollisionNumber property is use to know if the cube is
         * fully on a solid ground
         */
        protected int CollisionNumber = 0;

        public bool AllowInput = true;
        protected int FrameWithoutContact = 0;
        protected Vector3 LastMouvement;

        public void Fall()
        {
            IsFalling = true;
        }

        public bool GetIsFalling()
        {
            return IsFalling;
        }

        public void OnFlat()
        {
            IsFalling = false;
        }

        public void AllowMouvement()
        {
            AllowInput = true;
        }

        public void DenyMouvement()
        {
            AllowInput = false;
        }

        public bool GetAllowInput()
        {
            return AllowInput;
        }

        public void Reload()
        {
            if (transform.position.y < -10)
            {
                Scene scene = SceneManager.GetActiveScene();
                
                DontDestroyOnLoad(GameObject.Find("InGameDisplay"));

                SceneManager.LoadScene(scene.name);
            }
        }

        public void IncreaseMouvementNumber()
        {
            if (MouvementCounterObject == null)
            {
                MouvementCounterObject = GameObject.FindGameObjectWithTag("Counter").GetComponent<InGameUIManager>();
            }
            MouvementCounterObject.IncreaseMouvements();
        }

        public void Expulse()
        {
            if (++FrameWithoutContact >= 3)
            {
                Fall();
                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<Rigidbody>().isKinematic = false;
                if (LastMouvement == Vector3.back)
                    GetComponent<Rigidbody>().AddTorque(Vector3.left * 10);
                else if (LastMouvement == Vector3.forward)
                    GetComponent<Rigidbody>().AddTorque(Vector3.right * 10);
                else if (LastMouvement == Vector3.left)
                    GetComponent<Rigidbody>().AddTorque(Vector3.forward * 10);
                else if (LastMouvement == Vector3.right)
                    GetComponent<Rigidbody>().AddTorque(Vector3.back * 10);
            }
        }

        public abstract void TestStability();
        public abstract void Rotate(Vector3 direction, Vector3 rotation);

        public virtual void TestMouvement()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && GetAllowInput())
            {
                Rotate(Vector3.left, new Vector3(0, 0, 1));
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && GetAllowInput())
            {
                Rotate(Vector3.right, new Vector3(0, 0, -1));
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && GetAllowInput())
            {
                Rotate(Vector3.forward, new Vector3(1, 0, 0));
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && GetAllowInput())
            {
                Rotate(Vector3.back, new Vector3(-1, 0, 0));
            }
        }

    }
}
