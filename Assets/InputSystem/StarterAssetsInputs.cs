using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool fire;
		public bool nextWeapon;
        public bool startGame;
        public bool closeGame;

        [Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

        public void OnFire(InputValue value)
        {
            FireInput(value.isPressed);
        }

        public void OnNextWeapon(InputValue value)
        {
            NextWeaponInput(value.isPressed);
        }

        public void OnStartGame(InputValue value)
        {
            StartGameInput(value.isPressed);
        }

        public void OnCloseGame(InputValue value)
        {
            CloseGameInput(value.isPressed);
        }
#endif


        public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

        public void FireInput(bool newFireState)
        {
            fire = newFireState;
        }

        public void NextWeaponInput(bool newNextWeaponState)
        {
            nextWeapon = newNextWeaponState;
        }

        public void StartGameInput(bool newStartGameState)
        {
            startGame = newStartGameState;
        }

        public void CloseGameInput(bool newCloseGameState)
        {
            closeGame = newCloseGameState;
        }

        private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}