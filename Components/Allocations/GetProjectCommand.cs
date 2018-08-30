using System;
using System.Threading.Tasks;
using Steeltoe.CircuitBreaker.Hystrix;

namespace Allocations
{
    public class GetProjectCommand : HystrixCommand<ProjectInfo>
    {
        private readonly Func<long, Task<ProjectInfo>> _getProjectFn;
        private readonly Func<long, Task<ProjectInfo>> _getProjectFallbackFn;
        private readonly long _projectId;

        public GetProjectCommand(
            Func<long, Task<ProjectInfo>> getProjectFn,
            Func<long, Task<ProjectInfo>> getProjectFallbackFn,
            long projectId
        ) : base(HystrixCommandGroupKeyDefault.AsKey("ProjectClientGroup"))
        {
            _getProjectFn = getProjectFn;
            _getProjectFallbackFn = getProjectFallbackFn;
            _projectId = projectId;
        }

        protected override Task<ProjectInfo> RunAsync() => _getProjectFn(_projectId);
        protected override Task<ProjectInfo> RunFallbackAsync() => _getProjectFallbackFn(_projectId);
    }
}
