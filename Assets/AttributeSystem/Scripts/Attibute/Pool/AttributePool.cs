using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

namespace AttributeSystem
{
    [Serializable]
    public class AttributePool
    {
        private List<IAttributeSource> attributeSources = new List<IAttributeSource>();
        private List<IPoolMember> members = new List<IPoolMember>();

        [Button]
        public void AddSource(IAttributeSource source)
        {
            attributeSources.Add(source);
            NotifyAllMembersOfNewSource(source);
        }

        [Button]
        public void RemoveSource(IAttributeSource source)
        {
            if (attributeSources.Remove(source))
            {
                NotifyAllMembersOfRemovedSource(source);
            }
        }

        private void NotifyAllMembersOfNewSource(IAttributeSource source)
        {
            foreach (var member in members)
            {
                // Safe conversion and ensure members does not share mutable source
                HashSet<EnhanceAttributeInstance> attributes = source.GetAttributes() as HashSet<EnhanceAttributeInstance> ?? new HashSet<EnhanceAttributeInstance>(source.GetAttributes());
                member.AddAttributes(attributes);
            }
        }

        private void NotifyAllMembersOfRemovedSource(IAttributeSource source)
        {
            foreach (var member in members)
            {
                // Safe conversion and ensure members does not share mutable source
                HashSet<EnhanceAttributeInstance> attributes = source.GetAttributes() as HashSet<EnhanceAttributeInstance> ?? new HashSet<EnhanceAttributeInstance>(source.GetAttributes());
                member.RemoveAttributes(attributes);
            }
        }

        [Button]
        public void AddMember(IPoolMember member)
        {
            if (!members.Contains(member))
            {
                members.Add(member);
                member.AddAttributes(GetAllAttributes());
            }
        }

        [Button]
        public void RemoveMember(IPoolMember member)
        {
            if (members.Contains(member))
            {
                members.Remove(member);
                member.RemoveAttributes(GetAllAttributes());
            }
        }

        private HashSet<EnhanceAttributeInstance> GetAllAttributes()
        {
            return attributeSources?.SelectMany(source => source.GetAttributes()).ToHashSet() ?? new HashSet<EnhanceAttributeInstance>();
        }
    }
}