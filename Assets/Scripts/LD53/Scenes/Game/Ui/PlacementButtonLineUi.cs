using System.Collections;
using System.Collections.Generic;
using LD53.Data;
using LD53.Data.BeltsAndMachines;
using UnityEngine;

namespace LD53.Scenes.Game.Ui {
	public class PlacementButtonLineUi : MonoBehaviour {
		[SerializeField] protected PlacementButtonUi       _buttonPrefab;
		[SerializeField] protected List<PlacementButtonUi> _buttons = new List<PlacementButtonUi>();
		[SerializeField] protected AnimationCurve          _inCurve;
		[SerializeField] protected AnimationCurve          _outCurve;

		public IEnumerator Show(LevelDescriptor level, float speed) {
			while (_buttons.Count < level.availableMachineCounts.Count + 1) _buttons.Add(Instantiate(_buttonPrefab, transform));

			if (level.allowBeltPlacement) _buttons[0].SetUpForBelt();
			for (var i = 0; i < level.availableMachineCounts.Count; ++i) _buttons[i + 1].SetUpForMachine(level.availableMachineCounts[i]);

			return PlayAnimation(_inCurve, level.availableMachineCounts.Count + (level.allowBeltPlacement ? 1 : 0), speed);
		}

		public IEnumerator Hide(float speed) => PlayAnimation(_outCurve, _buttons.Count, speed);

		private IEnumerator PlayAnimation(AnimationCurve curve, int count, float speed) {
			var progress = -count * .5f;
			while (progress < 1) {
				for (var i = 0; i < count; ++i) _buttons[i].SetVerticalOffset(curve.Evaluate(Mathf.Clamp01(i * .5f + progress)));
				progress += Time.deltaTime * speed;
				yield return null;
			}
			for (var i = 0; i < count; ++i) _buttons[i].SetVerticalOffset(curve.Evaluate(1));
		}

		public void SetHidden() {
			foreach (var t in _buttons)
				t.SetVerticalOffset(_outCurve.Evaluate(1));
		}

		public void SetBeltSelected() {
			foreach (var button in _buttons) button.SetSelected(button.type == PlacementButtonUi.Type.Belt);
		}

		public void SetMachineSelected(IMachine machine) {
			foreach (var button in _buttons) button.SetSelected(button.type == PlacementButtonUi.Type.Machine && button.machine == machine);
		}
	}
}