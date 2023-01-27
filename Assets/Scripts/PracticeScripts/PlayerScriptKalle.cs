
using UnityEngine;

namespace ks
{
    public class PlayerScript : MonoBehaviour
    {
        public Rigidbody2D rb2d;
        bool isRightPressed, isLeftPressed;
        public float walkSpeed;

        void Update()
        {
            if (Input.GetKey(KeyCode.A))
                isLeftPressed = true;
            else
                isLeftPressed = false;

            if (Input.GetKey(KeyCode.D))
                isRightPressed = true;
            else
                isRightPressed = false;

            if (isRightPressed)
                MovePlayerRight();
            else if (isLeftPressed)
                MovePlayerLeft();
            else
                StopMovement();
        }

        //Movement scripts
        void MovePlayerLeft()
        {
            rb2d.velocity = new Vector2(-walkSpeed, 0);
        }
        void MovePlayerRight()
        {
            rb2d.velocity = new Vector2(walkSpeed, 0);
        }
        void StopMovement()
        {
            rb2d.velocity = new Vector2(0, 0);
        }

        //collisions
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Damage"))
            {
                Debug.Log("DAMAGE");
            }
        }

        void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Damage"))
            {
                Debug.Log("NO MORE DAMAGE");
            }
        }
    }
}
