using jp.illusive_isc.RuruneOptimizer;
using nadena.dev.ndmf;

[assembly: ExportsPlugin(typeof(IllRuruneOptimizerDifinition))]

namespace jp.illusive_isc.RuruneOptimizer
{
    public class IllRuruneOptimizerDifinition : Plugin<IllRuruneOptimizerDifinition>
    {
        public override string QualifiedName => "IllusoryOverride.IllRuruneOptimizer";
        public override string DisplayName => "RuruneOptimizer";

        protected override void Configure()
        {
            InPhase(BuildPhase.Resolving)
                .BeforePlugin("com.anatawa12.avatar-optimizer")
                .Run(IllRuruneOptimizerPass.Instance);
        }
    }
}
