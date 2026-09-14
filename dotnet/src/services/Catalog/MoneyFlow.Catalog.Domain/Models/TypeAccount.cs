using Shared.Kernel.Primitives;
using Shared.Kernel.ValueObjects;
using MoneyFlow.Catalog.Domain.ValueObjects.TypesAccounts;

namespace MoneyFlow.Catalog.Domain.Models
{
    public sealed class TypeAccount : Aggregate<TypeAccountId>
    {
        public TypeAccountName Name { get; private set; }

        private TypeAccount() { }
        private TypeAccount(TypeAccountName name) : base(TypeAccountId.New) => Name = name;

        public static TypeAccount Create(TypeAccountName name) => new (name);
    }
}