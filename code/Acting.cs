using Sandbox;
using Sandbox.Citizen;

public sealed class Acting : Component
{
	[Property]
	public CitizenAnimationHelper AnimationHelper { get; set; }

	[Property]
	public CitizenAnimationHelper.HoldTypes HoldType { get; set; } = CitizenAnimationHelper.HoldTypes.None;

	[Property]

	protected override void OnUpdate()
	{
		if ( AnimationHelper == null ) return;

		AnimationHelper.HoldType = HoldType;
	}
}
