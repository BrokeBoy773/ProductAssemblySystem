using ProductAssemblySystem.AssemblyManagement.Domain.ValueObjects;

namespace ProductAssemblySystem.AssemblyManagement.Domain.Entities
{
    public class AssemblySite
    {
        public Guid Id { get; }
        public Name Name { get; private set; } = null!;

        private readonly List<Set> _sets = [];
        public IReadOnlyList<Set> Sets => _sets;

        private readonly List<Part> _parts = [];
        public IReadOnlyList<Part> Parts => _parts;
    }
}
