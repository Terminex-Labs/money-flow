using Shared.Kernel.Primitives;
using Shared.Kernel.ValueObjects;
using MoneyFlow.Catalog.Domain.ValueObjects.TypesAccounts;

namespace MoneyFlow.Catalog.Domain.Models
{
    public sealed class TypeTransaction : Aggregate<TypeTransactionId>
    {
        public TypeTransactionName Name { get; private set; }

        private TypeTransaction() { }
        private TypeTransaction(TypeTransactionName name) : base(TypeTransactionId.New) => Name = name;

        public static TypeTransaction Create(TypeTransactionName name) => new (name);

        public void UpdateName(TypeTransactionName name) => Name = name;
    }
}