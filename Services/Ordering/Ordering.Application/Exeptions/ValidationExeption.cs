using FluentValidation.Results;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Exeptions
{
    public class ValidationExeption : ApplicationException
    {
        public IDictionary<string, string[]> Errors { get; set; }
        public ValidationExeption() : base("one or more validation failurd have occured.")
        {
            Errors = new ConcurrentDictionary<string, string[]>();
        }
        public ValidationExeption(IEnumerable<ValidationFailure> failures) : this()
        {
            Errors = failures.GroupBy(c => c.PropertyName, c => c.ErrorMessage)
                .ToDictionary(failorGroup => failorGroup.Key, failorGroup => failorGroup.ToArray());

        }
    }
}
