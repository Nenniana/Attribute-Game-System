using System.Collections.Generic;

namespace AttributeSystem
{
    public interface IPoolMember
    {
        AttributeManager AttributeManager { get; }
        List<BaseAttributeInstance> InherientBaseAttributes { get; }
        List<EnhanceAttributeInstance> InherientEnhanceAttributes { get; }
        float GetCalculatedAttributeValue(string mainAttributeName);
        void AddAttributes(HashSet<EnhanceAttributeInstance> poolAttributes);
        void RemoveAttributes(HashSet<EnhanceAttributeInstance> poolAttributes); 
    }
}