using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using System.Reflection;


namespace Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            ApplyMappingsFromAssembly(currentAssembly);

            CreateMap<Notification, ResponseNotification>();
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var mapFromType = typeof(IMapFrom<>);

            var mappingMethodName = nameof(IMapFrom<object>.Mapping);

            bool HasInterface(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == mapFromType;

            var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(HasInterface)).ToList();

            var argumentTypes = new Type[] { typeof(Profile) };

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod(mappingMethodName);

                if (methodInfo != null)
                {
                    methodInfo.Invoke(instance, [this]);
                }
                else
                {
                    var interfaces = type.GetInterfaces().Where(HasInterface).ToList();

                    if (interfaces.Count > 0)
                    {
                        foreach (var @interface in interfaces)
                        {
                            var interfaceMethodInfo = @interface.GetMethod(mappingMethodName, argumentTypes);

                            interfaceMethodInfo?.Invoke(instance, [this]);
                        }
                    }
                }
            }
        }
    }
}
