/// <summary>
/// This code snippet represents an interface called IAttributeSource in the AttributeSystem namespace. 
/// It defines two properties: SourceAttributes, which is a list of AttributeInstance objects, and GetAttributes, which returns a HashSet of AttributeInstance objects. 
/// The GetAttributes method is virtual and returns the SourceAttributes list converted to a HashSet. 
/// </summary>

using System.Collections.Generic;
using System.Linq;

namespace AttributeSystem
{
    public interface IAttributeSource
    {
        List<EnhanceAttributeInstance> SourceAttributes { get; }
        virtual HashSet<EnhanceAttributeInstance> GetAttributes() { return SourceAttributes.ToHashSet(); }
    }
}