using Sirenix.OdinInspector;
using UnityEngine;
using System.Linq;
using System;

namespace AttributeSystem
{
    [CreateAssetMenu(fileName = "CalculationStrategyGroup", menuName = "CalculationStrategies/StrategyGroup")]
    public class CalculationStrategyGroup : ScriptableObject
    {
        [ListDrawerSettings(ShowFoldout = true)]
        [InlineEditor(InlineEditorModes.FullEditor)]
        public CalculationStrategy[] strategies;
    }
}