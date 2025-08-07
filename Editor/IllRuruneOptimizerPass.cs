using nadena.dev.ndmf;
using UnityEngine;

namespace jp.illusive_isc.RuruneOptimizer
{
    public class IllRuruneOptimizerPass : Pass<IllRuruneOptimizerPass>
    {
        protected override void Execute(BuildContext context)
        {
            foreach (
                IllRuruneOptimizer IllRuruneOptimizer in context.AvatarRootObject.GetComponentsInChildren<IllRuruneOptimizer>()
            )
            {
                Object.DestroyImmediate(IllRuruneOptimizer.gameObject);
            }
        }
    }
}
