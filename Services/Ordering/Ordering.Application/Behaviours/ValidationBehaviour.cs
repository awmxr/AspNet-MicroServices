using FluentValidation;
using MediatR;
using ValidationExeption = Ordering.Application.Exeptions.ValidationExeption;

namespace Ordering.Application.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResult = await Task.WhenAll(_validators.Select(c => c.ValidateAsync(context, cancellationToken)));
                var failurs = validationResult.SelectMany(c => c.Errors).Where(c=> c!= null).ToList();
                if(failurs.Count > 0)
                {
                    throw new ValidationException(failurs);
                }
                
            }
            return await next();
        }
    }
}
