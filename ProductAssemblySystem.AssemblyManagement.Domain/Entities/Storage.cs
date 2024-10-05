using CSharpFunctionalExtensions;
using ProductAssemblySystem.AssemblyManagement.Domain.Auxiliaries;

namespace ProductAssemblySystem.AssemblyManagement.Domain.Entities
{
    public class Storage
    {
        public Guid Id { get; }

        private readonly List<PartQuantity> _storedParts = [];
        public IReadOnlyList<PartQuantity> StoredParts => _storedParts;

        private readonly List<SetQuantity> _storedSets = [];
        public IReadOnlyList<SetQuantity> StoredSets => _storedSets;

        public AssemblySite AssemblySite { get; } = null!;

        private Storage(Guid id)
        {
            Id = id;
        }

        public static Result<Storage> Create(Guid id)
        {
            Storage validStorage = new(id);

            return Result.Success(validStorage);
        }
    }
}
