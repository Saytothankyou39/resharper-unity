using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using JetBrains.Metadata.Reader.API;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Modules;

namespace JetBrains.ReSharper.Plugins.Unity
{
    public class UnityType
    {
        private readonly IClrTypeName myTypeName;
        private readonly Version myMinimumVersion;
        private readonly Version myMaximumVersion;
        private readonly IEnumerable<UnityEventFunction> myEventFunctions;

        public UnityType(IClrTypeName typeName, IEnumerable<UnityEventFunction> eventFunctions, Version minimumVersion, Version maximumVersion)
        {
            myTypeName = typeName;
            myMinimumVersion = minimumVersion;
            myMaximumVersion = maximumVersion;
            myEventFunctions = eventFunctions;
        }

        public IEnumerable<UnityEventFunction> GetEventFunctions(Version unityVersion)
        {
            return myEventFunctions.Where(f => f.SupportsVersion(unityVersion));
        }

        [CanBeNull]
        public ITypeElement GetTypeElement([NotNull] IPsiModule module)
        {
            var type = TypeFactory.CreateTypeByCLRName(myTypeName, module);
            return type.GetTypeElement();
        }

        public bool HasEventFunction([NotNull] IMethod method, Version unityVersion)
        {
            foreach (var function in myEventFunctions)
            {
                if (function.SupportsVersion(unityVersion) && function.Match(method) != EventFunctionMatch.NoMatch)
                    return true;
            }
            return false;
        }

        public bool SupportsVersion(Version unityVersion)
        {
            return myMinimumVersion <= unityVersion && unityVersion <= myMaximumVersion;
        }
    }
}