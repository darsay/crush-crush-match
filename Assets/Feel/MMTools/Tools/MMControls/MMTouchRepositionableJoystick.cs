using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

namespace MoreMountains.Tools
{
	/// <summary>
	/// Add this component to a UI rectangle and it'll act as a detection zone for a joystick.
	/// Note that this component extends the MMTouchJoystick class so you don't need to add another joystick to it. It's both the detection zone and the stick itself.
	/// </summary>
	[AddComponentMenu("More Mountains/Tools/Controls/MMTouchRepositionableJoystick")]
	public class MMTouchRepositionableJoystick : MMTouchJoystick, IPointerDownHandler
	{
		[Header("Dynamic Joystick")] 
		/// the canvas group to use as the joystick's knob
		public CanvasGroup KnobCanvasGroup;
		/// the canvas group to use as the joystick's background
		public CanvasGroup BackgroundCanvasGroup;
		/// if this is true, the joystick won't be able to travel beyond the bounds of the top level canvas
		public bool ConstrainToInitialRectangle = true;

		protected Vector3 _initialPosition;
		protected Vector3 _newPosition;
		protected CanvasGroup _knobCanvasGroup;
		protected RectTransform _rect;

		/// <summary>
		/// On Start, we instantiate our joystick's image if there's one
		/// </summary>
		protected override void Start()
		{
			base.Start();

			// we store the detection zone's initial position
			_initialPosition = GetComponent<RectTransform>().localPosition;
			_rect = GetComponent<RectTransform>();
		}

		public override void Initialize()
		{
			base.Initialize();
			SetKnobTransform(KnobCanvasGroup.transform);
			_canvasGroup = KnobCanvasGroup;
			_initialOpacity = _canvasGroup.alpha;
		}

		/// <summary>
		/// When the zone is pressed, we move our joystick accordingly
		/// </summary>
		/// <param name="data">Data.</param>
		public virtual void OnPointerDown(PointerEventData data)
		{
			// if we're in "screen space - camera" render mode
			if (ParentCanvasRenderMode == RenderMode.ScreenSpaceCamera)
			{
				_newPosition = TargetCamera.ScreenToWorldPoint(data.position);
			}
			// otherwise
			else
			{
				_newPosition = data.position;
			}
			_newPosition.z = this.transform.position.z;
			
			if (!WithinBounds())
			{
				return;
			}

			// we define a new neutral position
			BackgroundCanvasGroup.transform.position = _newPosition;
			SetNeutralPosition(_newPosition);
			_knobTransform.position = _newPosition;
		}

		/// <summary>
		/// Returns true if the joystick's new position is within the bounds of the top level canvas
		/// </summary>
		/// <returns></returns>
		protected virtual bool WithinBounds()
		{
			if (!ConstrainToInitialRectangle)
			{
				return true;
			}
			return RectTransformUtility.RectangleContainsScreenPoint(_rect, _newPosition);
		}

		/// <summary>
		/// When the player lets go of the stick, we restore our stick's position if needed
		/// </summary>
		/// <param name="eventData">Event data.</param>
		public override void OnEndDrag(PointerEventData eventData)
		{
			base.OnEndDrag(eventData);
		}
	}
}