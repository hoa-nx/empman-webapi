using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Common
{
    public class ReducedAutoMapper
    {
        private static ReducedAutoMapper instance;
        private Dictionary<object, object> mappingTypes;

        public static ReducedAutoMapper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ReducedAutoMapper();
                    instance.mappingTypes = new Dictionary<object, object>();
                }
                return instance;
            }
        }

        public Dictionary<object, object> MappingTypes
        {
            get
            {
                return this.mappingTypes;
            }
            set
            {
                this.mappingTypes = value;
            }
        }

        public void CreateMap<TSource, TDestination>()
            where TSource : new()
            where TDestination : new()
        {
            if (!this.MappingTypes.ContainsKey(typeof(TSource)))
            {
                this.MappingTypes.Add(typeof(TSource), typeof(TDestination));
            }
        }

        public TDestination Map<TSource, TDestination>(
            TSource realObject,
            TDestination dtoObject = default(TDestination),
            Dictionary<object, object> alreadyInitializedObjects = null,
            bool shouldMapInnerEntities = true)
            where TSource : class, new()
            where TDestination : class, new()
        {
            if (realObject == null)
            {
                return null;
            }
            if (alreadyInitializedObjects == null)
            {
                alreadyInitializedObjects = new Dictionary<object, object>();
            }
            if (dtoObject == null)
            {
                dtoObject = new TDestination();
            }

            var realObjectType = realObject.GetType();
            PropertyInfo[] properties = realObjectType.GetProperties();
            foreach (PropertyInfo currentRealProperty in properties)
            {
                PropertyInfo currentDtoProperty = dtoObject.GetType().GetProperty(currentRealProperty.Name);
                if (currentDtoProperty == null)
                {
                    ////Debug.WriteLine("The property {0} was not found in the DTO object in order to be mapped. Because of that we skip to map it.", currentRealProperty.Name);
                }
                else
                {
                    if (this.MappingTypes.ContainsKey(currentRealProperty.PropertyType) && shouldMapInnerEntities)
                    {
                        object mapToObject = this.mappingTypes[currentRealProperty.PropertyType];
                        var types = new Type[] { currentRealProperty.PropertyType, (Type)mapToObject };
                        MethodInfo method = GetType().GetMethod("Map").MakeGenericMethod(types);
                        var realObjectPropertyValue = currentRealProperty.GetValue(realObject, null);
                        var objects = new object[]
                        {
                            realObjectPropertyValue,
                            null,
                            alreadyInitializedObjects,
                            shouldMapInnerEntities
                        };
                        if (objects != null && realObjectPropertyValue != null)
                        {
                            if (alreadyInitializedObjects.ContainsKey(realObjectPropertyValue) && currentDtoProperty.CanWrite)
                            {
                                // Set the cached version of the same object (optimization)
                                currentDtoProperty.SetValue(dtoObject, alreadyInitializedObjects[realObjectPropertyValue]);
                            }
                            else
                            {
                                // Add the object to cached objects collection.
                                alreadyInitializedObjects.Add(realObjectPropertyValue, null);
                                // Recursively call Map method again to get the new proxy object.
                                var newProxyProperty = method.Invoke(this, objects);
                                if (currentDtoProperty.CanWrite)
                                {
                                    currentDtoProperty.SetValue(dtoObject, newProxyProperty);
                                }

                                if (alreadyInitializedObjects.ContainsKey(realObjectPropertyValue) && alreadyInitializedObjects[realObjectPropertyValue] == null)
                                {
                                    alreadyInitializedObjects[realObjectPropertyValue] = newProxyProperty;
                                }
                            }
                        }
                        else if (realObjectPropertyValue == null && currentDtoProperty.CanWrite)
                        {
                            // If the original value of the object was null set null to the destination property.
                            currentDtoProperty.SetValue(dtoObject, null);
                        }
                    }
                    else if (!this.MappingTypes.ContainsKey(currentRealProperty.PropertyType))
                    {
                        // If the property is not custom type just set normally the value.
                        if (currentDtoProperty.CanWrite)
                        {
                            currentDtoProperty.SetValue(dtoObject, currentRealProperty.GetValue(realObject, null));
                        }
                    }
                }
            }

            return dtoObject;
        }

        public List<TDestination> MapList<TSource, TDestination>(List<TSource> realObjects, Dictionary<object, object> alreadyInitializedObjects = null)
            where TSource : class, new()
            where TDestination : class, new()
        {
            List<TDestination> mappedEntities = new List<TDestination>();
            foreach (var currentRealObject in realObjects)
            {
                TDestination currentMappedItem = this.Map<TSource, TDestination>(currentRealObject, alreadyInitializedObjects: alreadyInitializedObjects);
                mappedEntities.Add(currentMappedItem);
            }

            return mappedEntities;
        }

    }
}
