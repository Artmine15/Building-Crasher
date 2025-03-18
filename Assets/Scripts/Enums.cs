namespace Artmine15.HappyBirthday.v3.Gisha
{
    public enum BlockTypes { Last, Basic, Broken, Rest }
    public enum BlockState { Untouchable, Waiting, Activated, Throwing, Acceleration, Falling }
    public enum BlockConfirmationCondition { OnActivation, OnThrown }

    public enum MusicState { Stopped, FadingIn, Playing, FadingOut }
    public enum CameraState { Default, MovingDown, Waiting, MovingUp }
    public enum EnemyFallingState { OnBlock, InAir, Fell, Killed }

    public enum PlayerJumpingState { None, Jumping, Falling }

    public enum UpdateMehod { Update, FixedUpdate, LateUpdate }
}
