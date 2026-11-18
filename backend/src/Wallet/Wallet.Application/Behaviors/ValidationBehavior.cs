using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Application.Behaviors
{
	public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	where TRequest : notnull
	{
		private readonly IEnumerable<IValidator<TRequest>> _validators;

		public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
		{
			_validators = validators;
		}

		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			if (!_validators.Any())
			{
				return await next();
			}

			var context = new ValidationContext<TRequest>(request);

			var errors = _validators
				.Select(v => v.Validate(context))
				.SelectMany(result => result.Errors)
				.Where(f => f != null)
				.ToList();

			if (errors.Any())
			{
				// Якщо є помилки, кидаємо виняток (ми його перехопимо на рівні API)
				throw new ValidationException(errors);
			}

			// Якщо все добре, передаємо керування хендлеру (DepositFundsCommandHandler)
			return await next();
		}
	}
}
