using System.Collections.Generic;
using LD53.Data.Common;
using UnityEngine;
using UnityEngine.Events;
using Utils.Libraries;

namespace LD53.Data.BeltsAndMachines {
	public interface IMachine {
		public class Event : UnityEvent<IMachine> { }

		string                 name                   { get; }
		GameObject             gameObject             { get; }
		bool                   hasUpperHandOnBelt     { get; }
		Direction              beltDirection          { get; }
		IEnumerable<Direction> allPotentialDirections { get; }
		BaseMachine            baseMachine            { get; }
		Sprite                 uiSprite               => Sprites.Of($"ui.placement.machine.{name}");
		public static Event    onTileDirectionChanged { get; } = new Event();
	}
}