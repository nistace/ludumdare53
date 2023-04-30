using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace LD53.Inputs {
	public class MachineInputControl : MonoBehaviour {
		private HashSet<char> keyDowns { get; } = new HashSet<char>();

		private static IEnumerable<(char c, KeyControl control)> keys { get; } = new[] {
			('a', Keyboard.current.aKey),
			('z', Keyboard.current.zKey),
			('e', Keyboard.current.eKey),
			('r', Keyboard.current.rKey),
			('t', Keyboard.current.tKey),
			('y', Keyboard.current.yKey),
			('u', Keyboard.current.uKey),
			('i', Keyboard.current.iKey),
			('o', Keyboard.current.oKey),
			('p', Keyboard.current.pKey),
			('q', Keyboard.current.qKey),
			('s', Keyboard.current.sKey),
			('d', Keyboard.current.dKey),
			('f', Keyboard.current.fKey),
			('g', Keyboard.current.gKey),
			('h', Keyboard.current.hKey),
			('j', Keyboard.current.jKey),
			('k', Keyboard.current.kKey),
			('l', Keyboard.current.lKey),
			('m', Keyboard.current.mKey),
			('w', Keyboard.current.wKey),
			('x', Keyboard.current.xKey),
			('c', Keyboard.current.cKey),
			('v', Keyboard.current.vKey),
			('b', Keyboard.current.bKey),
			('n', Keyboard.current.nKey)
		};

		private bool changedThisFrame { get; set; }

		public UnityEvent onKeyPressChanged { get; } = new UnityEvent();

		private void Update() {
			changedThisFrame = false;
			foreach (var key in keys) {
				if (key.control.wasPressedThisFrame) {
					keyDowns.Add(key.c);
					changedThisFrame = true;
				}
				if (key.control.wasReleasedThisFrame) {
					keyDowns.Remove(key.c);
					changedThisFrame = true;
				}
			}
			if (changedThisFrame) {
				onKeyPressChanged.Invoke();
			}
		}

		public bool IsDown(char c) => keyDowns.Contains(c);
	}
}