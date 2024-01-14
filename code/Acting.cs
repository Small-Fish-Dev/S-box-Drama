using Sandbox;
using Sandbox.Citizen;
using static Sandbox.Citizen.CitizenAnimationHelper;

public sealed class Acting : Component
{
	[Property]
	public SkinnedModelRenderer Actor { get; set; }

	[Property]
	public CitizenAnimationHelper AnimationHelper { get; set; }

	public enum FaceType
	{
		[Icon( "sentiment_neutral" )]
		None,

		[Icon( "mood" )]
		Smile,

		[Icon( "sentiment_dissatisfied" )]
		Frown,

		[Icon( "mood" )]
		Surprise,

		[Icon( "mood_bad" )]
		Sad,

		[Icon( "mood_bad" )]
		Angry
	}

	[Property]
	public FaceType Emotion { get; set; } = FaceType.None;

	[Property]
	public HoldTypes HoldType { get; set; } = HoldTypes.None;

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

	[Property]
	public bool IsGrounded { get; set; } = true;

	[Property]
	public bool IsSwimming { get; set; } = false;

	[Property]
	public bool IsClimbing { get; set; } = false;

	[Property]
	public bool IsNoclipping { get; set; } = false;

	[Property]
	public bool IsWeaponLowered { get; set; } = false;

	[Property]
	public bool IsSitting { get; set; } = false;

	[Property]
	public SittingStyle SittingStyle { get; set; } = SittingStyle.None;

	protected override void OnUpdate()
	{
		if ( AnimationHelper == null ) return;
		if ( Actor == null ) return;

		AnimationHelper.HoldType = HoldType;
		AnimationHelper.DuckLevel = DuckLevel;
		AnimationHelper.IsGrounded = IsGrounded;
		AnimationHelper.IsSwimming = IsSwimming;
		AnimationHelper.IsClimbing = IsClimbing;
		AnimationHelper.IsNoclipping = IsNoclipping;
		AnimationHelper.IsWeaponLowered = IsWeaponLowered;
		AnimationHelper.IsSitting = IsSitting;
		AnimationHelper.Sitting = SittingStyle;

		Actor.Set( "face_override", (int)Emotion );

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
