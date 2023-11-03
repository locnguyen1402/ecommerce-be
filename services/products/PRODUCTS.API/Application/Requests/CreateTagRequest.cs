namespace ECommerce.Products.Api.Application.Requests;

public class CreateTagRequest : IRequest<Tag>
{
    public string Value { get; set; } = null!;
}

public class CreateTagRequestValidator : AbstractValidator<CreateTagRequest>
{
    public CreateTagRequestValidator()
    {
        RuleFor(b => b.Value)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100);
    }
}

public class CreateTagRequestHandler : IRequestHandler<CreateTagRequest, Tag>
{
    private readonly ITagRepository _tagRepository;
    public CreateTagRequestHandler(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }
    public async Task<Tag> Handle(CreateTagRequest request, CancellationToken cancellationToken)
    {
        var existedTag = await _tagRepository.Query.FirstOrDefaultAsync(t => t.Value == request.Value, cancellationToken);

        if (existedTag != null)
        {
            return existedTag;
        }

        var item = new Tag(request.Value);
        _tagRepository.Add(item);

        await _tagRepository.SaveChangesAsync();

        return item;
    }
}