
namespace Sandbox
{
	public class CarAnimator : PawnAnimator
	{
		public override void Simulate()
		{
			var player = Pawn as Player;

			ResetParams();

			SetParam( "b_grounded", true );
			SetParam( "b_sit", true );

			var eyeAngles = (player.Rotation.Inverse * player.EyeRotation).Angles();
			eyeAngles.pitch = eyeAngles.pitch.Clamp( -25, 70 );
			eyeAngles.yaw = eyeAngles.yaw.Clamp( -90, 90 );

			var aimPos = player.EyePosition + (player.Rotation * Rotation.From( eyeAngles )).Forward * 200;

			SetLookAt( "aim_eyes", aimPos );
			SetLookAt( "aim_head", aimPos );
			SetLookAt( "aim_body", aimPos );

			if ( player.ActiveChild is BaseCarriable carry )
			{
				carry.SimulateAnimator( this );
			}
			else
			{
				SetParam( "holdtype", 0 );
				SetParam( "aim_body_weight", 0.5f );
			}
		}
	}
}
