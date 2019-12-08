using LaYumba.Functional;

namespace Subscription.Domain
{
    public class Errors
    {
        public static PinsDoNotMatchError PinsDoNotMatch => new PinsDoNotMatchError();
        public static PinRulesViolationError PinRulesViolation => new PinRulesViolationError();
        public static PinIncorrectError PinIncorrect => new PinIncorrectError();

        public sealed class PinsDoNotMatchError : Error
        {
            public override string Message { get; } = "PINs don't match.";
        }

        public sealed class PinRulesViolationError : Error
        {
            public override string Message { get; } = "PIN must be at least 4 and not more than 12 characters long.";
        }

        public sealed class PinIncorrectError : Error
        {
            public override string Message { get; } = "PIN incorrect.";
        }
    }
}