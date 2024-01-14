using Sandbox;
using Sandbox.Citizen;

public sealed class Acting : Component
{
	[Property]
	public SkinnedModelRenderer Actor { get; set; }

	[Property]
	public CitizenAnimationHelper AnimationHelper { get; set; }

	[Property]
	public CitizenAnimationHelper.HoldTypes HoldType { get; set; } = CitizenAnimationHelper.HoldTypes.None;

	[Property]
	public Model HoldingLeft { get; set; }

	[Property]
	public Angles LeftAngleOffset { get; set; }

	[Property]
	public Vector3 LeftPositionOffset { get; set; }

	[Property]
	public Model HoldingRight { get; set; }

	[Property]
	public Angles RightAngleOffset { get; set; }

	[Property]
	public Vector3 RightPositionOffset { get; set; }

	[Property]
	[Range( 0f, 2f, 0.1f )]
	public float DuckLevel { get; set; } = 0f;

	protected override void OnUpdate()
	{
		if ( AnimationHelper == null ) return;
		if ( Actor == null ) return;

		AnimationHelper.HoldType = HoldType;
		AnimationHelper.DuckLevel = DuckLevel;

		var draw = Gizmo.Draw;

		if ( HoldingLeft != null )
		{
			var handTransform = Actor.GetBoneObject( 15 ).Transform.World;
			var offsetTransform = handTransform.WithPosition( handTransform.PointToWorld( LeftPositionOffset ) );
			var rotatedTransform = offsetTransform.WithRotation( offsetTransform.Rotation * LeftAngleOffset );
			draw.Model( HoldingLeft, rotatedTransform );
		}

		if ( HoldingRight != null )
		{
			var handTransform = Actor.GetBoneObject( 10 ).Transform.World;
			var offsetTransform = handTransform.WithPosition( handTransform.PointToWorld( RightPositionOffset ) );
			var rotatedTransform = offsetTransform.WithRotation( offsetTransform.Rotation * RightAngleOffset );
			draw.Model( HoldingRight, rotatedTransform );
		}
	}

	protected override void OnAwake()
	{
		base.OnAwake();
	}
}
