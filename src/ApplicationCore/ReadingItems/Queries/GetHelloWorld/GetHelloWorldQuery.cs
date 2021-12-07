using MediatR;

namespace Application.ReadingItems.Queries.GetHelloWorld;

public class GetHelloWorldQuery : IRequest<string>
{
    public string? Test { get; set; }
}

public class GetHelloWorldQueryHandler : IRequestHandler<GetHelloWorldQuery, string>
{
    public Task<string> Handle(GetHelloWorldQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult("Hello World!");
    }
}
