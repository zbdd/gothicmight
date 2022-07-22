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
		public Vector2 uiLook;
		public bool jump;
		public bool sprint;
		public bool interact;
		public bool menuInteract = false;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

		public HUDController hud;

        private void Update()
        {
            if (!hud.invIsOpen && cursorLocked == false) { cursorLocked = true; SetCursorState(cursorLocked); } // handle left-clicks occuring that are not captured by this input controller
        }

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
			if (!hud.invIsOpen) JumpInput(value.isPressed);
			else menuInteract = true;
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
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
		
		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}

		public void OnUILook(InputValue value)
        {
			uiLook = value.Get<Vector2>();
        }

		public void OnInteract(InputValue value)
        {
			interact = true;
        }

		public void OnOpenMenu(InputValue value)
        {
			if (hud.invIsOpen)
			{
				cursorLocked = true;
				hud.SetInventoryOpen(false);
			}
			else { cursorLocked = false; hud.SetInventoryOpen(true); }

			SetCursorState(cursorLocked);
			hud.SetInventoryOpen(!cursorLocked);
		}

		public void OnUIEnter(InputValue value)
        {
			menuInteract = true;
        }

		public void OnAny(InputValue value)
		{
			hud.OnAnyKey();
		}
	}
	
}