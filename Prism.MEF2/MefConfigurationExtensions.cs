using System.Composition.Convention;

namespace Prism.MEF2
{
    public static class MefConfigurationExtensions
    {
        public static PartConventionBuilder ExportAllInterfaces<TExportedType>(this ConventionBuilder builder, bool registerAsSingletone)
        {
            var builderPart = builder.ForType<TExportedType>().ExportInterfaces();

            if (registerAsSingletone) builderPart.Shared();

            return builderPart;
        }
    }
}