using System.Threading;

namespace CodeExcercise.Common.Helpers
{
    public static class CancelationTokenHelper
    {
        public static CancellationToken CreateCancelationToken()
        {
            return new CancellationTokenSource().Token;
        }
    }
}
